using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Client.Contracts;
using Client.Contracts.ViewModels;
using Client.Contracts.ViewModels.AddPlatform;

namespace Client.Http
{
    internal class HttpApiClient : IApiClient
    {
        public static readonly HttpClient _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("http://localhost:38850/api/platforms/"),
        };

        public async Task<IReadOnlyCollection<PlatformViewModel>> GetPlatforms()
        {
            return await _httpClient.GetFromJsonAsync<IReadOnlyCollection<PlatformViewModel>>(string.Empty);
        }

        public async Task<IReadOnlyCollection<CommandViewModel>> GetCommands()
        {
            return await _httpClient.GetFromJsonAsync<IReadOnlyCollection<CommandViewModel>>("commands");
        }

        public async Task<AddPlatformResponse> AddPlatform(AddPlatformRequest request, CancellationToken cancellationToken)
        {
            var json = JsonSerializer.Serialize(request);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var message = await _httpClient.PostAsync(requestUri: string.Empty, content, cancellationToken);

            var response = await message.Content.ReadAsStringAsync(cancellationToken);

            return JsonSerializer.Deserialize<AddPlatformResponse>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
        }

        public class NullDisposable : IDisposable
        {
            public void Dispose()
            {
            }
        }
        public IDisposable SubscribeToAddPlatformStream(Action<PlatformAddedEvent> action, CancellationToken cancellationToken)
        {
            // throw new NotSupportedException();

            return new NullDisposable();
        }
    }
}
