using FunWithOAuth.OAuth.DefaultOAuthService;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;

namespace FunWithOAuth.OAuth.GoogleOAuthService
{
    public class GoogleConnectionService : OAuthConnectionService
    {
        public GoogleConnectionService(
            IOauthTokenRepository oAuthTokenRepository,
            IOptions<GoogleOptions> options,
            ILogger<GoogleConnectionService> logger,
            IHttpClientFactory httpClientFactory)
            : base(oAuthTokenRepository, options.Value, OAuthAppType.Google, logger, httpClientFactory, "Google")
        {
        }

        protected override async Task<bool> RevokeToken(string token)
        {
            Logger.LogInformation("Revoke Google OAuth token");

            var client = _httpClientFactory.CreateClient(HttpClientName);
            var res = await client.PostAsync($"{_options.RevokeTokenUrl}?token={token}", null);

            return res.IsSuccessStatusCode;
        }

        protected override OAuthToken ParseResponse(string content, string redirectUri)
        {
            Logger.LogInformation("Google OAuth connection established");

            var response = JsonConvert.DeserializeObject<GoogleAccessTokenResponse>(content);
            return new OAuthToken
            {
                OAuthAppType = OAuthAppType.Google,
                // UserId = _userContext.UserId,
                AccessToken = response.AccessToken,
                RefreshToken = response.RefreshToken,
                ExpirationDate = DateTime.UtcNow.AddSeconds(response.ExpiresIn),
                LastError = response.Error,
                LastErrorDescription = response.ErrorDescription,
                RedirectUri = redirectUri,
            };
        }

        protected override OAuthToken ParseError(string content, string redirectUri, HttpStatusCode statusCode)
        {
            var errorResponse = JsonConvert.DeserializeObject<DefaultErrorResponse>(content);

            Logger.LogError("Cannot terminate the oauth connection process with Google ({RedirectUri}, {StatusCode}, {@ErrorResponse})", redirectUri, statusCode, errorResponse);

            return new OAuthToken
            {
                OAuthAppType = OAuthAppType.Google,
                // UserId = _userContext.UserId,
                AccessToken = "",
                RefreshToken = "",
                ExpirationDate = DateTime.UtcNow,
                LastError = errorResponse?.Error,
                LastErrorDescription = errorResponse?.ErrorDescription,
                RedirectUri = redirectUri,
            };
        }

        protected override void ParseRefresh(string content, OAuthToken token)
        {
            var response = JsonConvert.DeserializeObject<GoogleAccessTokenResponse>(content);

            if (string.IsNullOrEmpty(response?.AccessToken))
            {
                var errorResponse = JsonConvert.DeserializeObject<DefaultErrorResponse>(content);
                Logger.LogError($"Cannot refresh oauth token for Google: {errorResponse}");

                throw new Exception(errorResponse?.Error);
            }

            token.AccessToken = response.AccessToken;
            token.ExpirationDate = DateTime.UtcNow.AddSeconds(response.ExpiresIn);
            token.LastError = response.Error;
            token.LastErrorDescription = response.ErrorDescription;

            // NOTE: using the same refresh token
            // token.RefreshToken = response.RefreshToken;
        }

    }
}
