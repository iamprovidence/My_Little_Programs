namespace FunWithOAuth.OAuth
{
    public interface IOAuthOptions
    {
        string ClientId { get; set; }
        string ClientSecret { get; set; }
        string GetAccessTokenUrl { get; set; }
        string RefreshTokenUrl { get; set; }
        string RevokeTokenUrl { get; set; }
    }
}
