using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FunWithOAuth.OAuth.DefaultOAuthService
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class DefaultErrorResponse
    {
        public string Error { get; set; }

        public string ErrorDescription { get; set; }
    }
}
