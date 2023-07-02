namespace FunWithOAuth.OAuth.GoogleOAuthService
{
    public class GoogleOptions : IOAuthOptions
    {
        public string GetAccessTokenUrl { get; set; }
        public string RefreshTokenUrl { get; set; }
        public string RevokeTokenUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }

}
