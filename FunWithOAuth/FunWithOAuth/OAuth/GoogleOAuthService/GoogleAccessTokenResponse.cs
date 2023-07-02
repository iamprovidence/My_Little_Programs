using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FunWithOAuth.OAuth.GoogleOAuthService
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class GoogleAccessTokenResponse
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public int ExpiresIn { get; set; }

        public string Error { get; set; }

        public string ErrorDescription { get; set; }
    }
}
