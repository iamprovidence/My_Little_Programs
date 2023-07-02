using Newtonsoft.Json;
using System.Net;

namespace FunWithOAuth.OAuth.DefaultOAuthService
{

    public abstract class OAuthConnectionService : IOAuthConnectionService
    {
        protected readonly IOAuthOptions _options;
        protected readonly string HttpClientName;
        protected readonly IHttpClientFactory _httpClientFactory;

        protected readonly IOauthTokenRepository _oAuthTokenRepository;
        protected readonly OAuthAppType _oAuthAppType;
        protected readonly ILogger<OAuthConnectionService> Logger;

        protected OAuthConnectionService(
            IOauthTokenRepository oAuthTokenRepository,
            IOAuthOptions options,
            OAuthAppType oAuthAppType,
            ILogger<OAuthConnectionService> logger,
            IHttpClientFactory httpClientFactory,
            string httpClientName)
        {
            _oAuthTokenRepository = oAuthTokenRepository;
            _oAuthAppType = oAuthAppType;
            Logger = logger;
            _httpClientFactory = httpClientFactory;
            HttpClientName = httpClientName;
            _options = options;
        }

        public virtual async Task<OAuthToken> RequestAccessToken(string code, string redirectUri)
        {
            var client = _httpClientFactory.CreateClient(HttpClientName);
            var postContent = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
            {
                new("grant_type", "authorization_code"),
                new("client_id", _options.ClientId),
                new("client_secret", _options.ClientSecret),
                new("redirect_uri", redirectUri),
                new("code", code),
            });

            var response = await client.PostAsync(_options.GetAccessTokenUrl, postContent);
            var content = await response.Content.ReadAsStringAsync();

            OAuthToken token = null;
            if (response.IsSuccessStatusCode)
            {
                token = ParseResponse(content, redirectUri);
            }
            else
            {
                token = ParseError(content, redirectUri, response.StatusCode);
            }

            await _oAuthTokenRepository.DeleteAllAsync(token.OAuthAppType, token.UserId);
            await _oAuthTokenRepository.AddAsync(token);

            await OnConnected(token, content);

            return token;
        }

        public async Task<OAuthToken> RefreshAccessToken(OAuthToken token)
        {
            var client = _httpClientFactory.CreateClient(HttpClientName);
            HttpContent c = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
            {
                new("grant_type", "refresh_token"),
                new("client_id", _options.ClientId),
                new("client_secret", _options.ClientSecret),
                new("redirect_uri", token.RedirectUri),
                new("refresh_token", token.RefreshToken)
            });

            var res = await client.PostAsync(_options.RefreshTokenUrl, c);
            var content = await res.Content.ReadAsStringAsync();

            ParseRefresh(content, token);
            await _oAuthTokenRepository.UpdateAsync(token);

            return token;
        }

        public virtual async Task<OAuthToken> GetAccessToken()
        {
            var token = await _oAuthTokenRepository.GetByAppTypeAsync(_oAuthAppType);
            if (token == null)
            {
                return null;
            }

            // Lead a 5 minute margin before expiration, so we have 5 minutes to use this key
            if (token.ExpirationDate <= DateTime.UtcNow.AddMinutes(5))
            {
                return await RefreshAccessToken(token);
            }

            return token;
        }

        public async Task Disconnect()
        {
            var token = await _oAuthTokenRepository.GetByAppTypeAsync(_oAuthAppType);
            if (token == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(token.RefreshToken) && await RevokeToken(token.RefreshToken))
            {
                await _oAuthTokenRepository.DeleteAllAsync(_oAuthAppType);
                await OnDisconnected();
            }
        }

        protected abstract Task<bool> RevokeToken(string token);

        protected virtual OAuthToken ParseResponse(string content, string redirectUri)
        {
            Logger.LogInformation("Google OAuth connection established");

            var response = JsonConvert.DeserializeObject<DefaultAccessTokenResponse>(content)!;
            return new OAuthToken
            {
                OAuthAppType = OAuthAppType.Google,
                UserId = default,
                AccessToken = response.AccessToken,
                RefreshToken = response.RefreshToken,
                ExpirationDate = DateTime.UtcNow.AddSeconds(response.ExpiresIn),
                LastError = default,
                LastErrorDescription = default,
                RedirectUri = redirectUri,
            };
        }

        protected abstract OAuthToken ParseError(string content, string redirectUri, HttpStatusCode statusCode);

        protected virtual void ParseRefresh(string content, OAuthToken token)
        {
            var response = JsonConvert.DeserializeObject<DefaultAccessTokenResponse>(content);

            if (string.IsNullOrEmpty(response?.AccessToken))
            {
                var errorResponse = JsonConvert.DeserializeObject<DefaultErrorResponse>(content);
                Logger.LogError($"Cannot refresh oauth token for {_oAuthAppType}: {errorResponse}");

                throw new Exception(errorResponse?.Error);
            }

            token.AccessToken = response.AccessToken;
            token.ExpirationDate = DateTime.UtcNow.AddSeconds(response.ExpiresIn);
            token.RefreshToken = response.RefreshToken;
        }

        protected virtual Task OnConnected(OAuthToken token, string content)
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnDisconnected()
        {
            return Task.CompletedTask;
        }
    }
}
