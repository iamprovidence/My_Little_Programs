namespace FunWithOAuth.OAuth
{
    public class OAuthToken
    {
        public int Id { get; set; }
        public OAuthAppType OAuthAppType { get; set; }
        public int UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string RedirectUri { get; set; }
        public string LastError { get; set; }
        public string LastErrorDescription { get; set; }
    }
}
