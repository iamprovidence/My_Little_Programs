namespace FunWithOAuth.OAuth
{
    public interface IOAuthConnectionService
    {
        Task<OAuthToken> RequestAccessToken(string code, string redirectUri);
        Task<OAuthToken> RefreshAccessToken(OAuthToken token);

        /// <summary>
        /// Get Access token for the OAuth connection or null if there is no token.
        /// </summary>
        /// <returns></returns>
        Task<OAuthToken> GetAccessToken();
        Task Disconnect();
    }

}
