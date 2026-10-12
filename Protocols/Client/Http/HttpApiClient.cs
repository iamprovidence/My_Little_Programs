using Client.Contracts;
using Client.Contracts.ViewModels;
using Client.Contracts.ViewModels.AddPlatform;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Http
{
    internal class HttpApiClient : IApiClient
    {
        // server accepts only HTTP/2 (see Kestrel:EndpointDefaults:Protocols), so over plain http we have to force h2c
        private static readonly HttpClient _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("http://localhost:38850/api/platforms/"),
            DefaultRequestVersion = HttpVersion.Version20,
            DefaultVersionPolicy = HttpVersionPolicy.RequestVersionExact,
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
            using var message = await _httpClient.PostAsJsonAsync(requestUri: string.Empty, request, cancellationToken);

            message.EnsureSuccessStatusCode();

            return await message.Content.ReadFromJsonAsync<AddPlatformResponse>(cancellationToken: cancellationToken);
        }

        public IDisposable SubscribeToAddPlatformStream(Action<PlatformAddedEvent> action, CancellationToken cancellationToken)
        {
            // plain HTTP has no push, so subscription is not supported
            return new NullDisposable();
        }

        private class NullDisposable : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}
