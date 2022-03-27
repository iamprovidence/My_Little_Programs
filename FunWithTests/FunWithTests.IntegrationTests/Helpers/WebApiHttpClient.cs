using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FunWithTests.IntegrationTests.Helpers
{
    public class WebApiHttpClient : HttpClient
    {
        private static string _url;

        public WebApiHttpClient(string url)
        {
            _url = url;
            BaseAddress = new Uri(url);
        }

        public async Task<SwaggerIdentityHelper> Authenticate(string userName = "admin", string password = "admin")
        {
            var identity = SwaggerIdentityHelper.Create(_url, userName, password);

            var accessToken = await identity.GetAcceessToken();

            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return identity;
        }

        public Task<HttpResponseMessage> PostAsync(string uri)
        {
            return PostAsync(uri, null);
        }

        public Task<HttpResponseMessage> PostAsync<T>(string uri, T request)
        {
            var serializedRequest = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(serializedRequest);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return base.PostAsync(uri, stringContent);
        }
    }
}
