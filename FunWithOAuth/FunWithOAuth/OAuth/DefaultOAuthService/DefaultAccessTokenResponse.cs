using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FunWithOAuth.OAuth.DefaultOAuthService
{

    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class DefaultAccessTokenResponse
    {
        //public string TokenType { get; set; }
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public int ExpiresIn { get; set; }

        public string Scope { get; set; }
    }
}
