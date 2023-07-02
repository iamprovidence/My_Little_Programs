using FunWithOAuth.OAuth.GoogleOAuthService;

namespace FunWithOAuth.OAuth
{
    public class OAuthConnectionServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public OAuthConnectionServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IOAuthConnectionService Create(OAuthAppType oauthAppType)
        {
            return oauthAppType switch
            {
                OAuthAppType.Google => _serviceProvider.GetRequiredService<GoogleConnectionService>(),

                _ => throw new NotImplementedException()
            };
        }
    }
}
