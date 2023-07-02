namespace FunWithOAuth.OAuth
{
    public interface IOauthTokenRepository
    {
        Task AddAsync(OAuthToken token);
        Task UpdateAsync(OAuthToken token);
        Task<OAuthToken> GetAsync(OAuthAppType type, int userId);
        Task<OAuthToken> GetByAppTypeAsync(OAuthAppType type);

        Task<bool> DeleteAllAsync(OAuthAppType token);
        Task<bool> DeleteAllAsync(OAuthAppType token, int userId);
    }
}
