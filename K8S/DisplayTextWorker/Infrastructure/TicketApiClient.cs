using System.Net.Http.Json;
using DisplayTextWorker.Application;
using DisplayTextWorker.Infrastructure.Protos;
using Grpc.Net.Client;
using TicketApi.Contracts;
using TicketApi.Contracts.ViewModels;

namespace DisplayTextWorker.Infrastructure
{
    public class TicketApiClient : ITicketApiClient
    {
        public const string HttpClientName = "ticket-api";

        private readonly HttpClient _httpClient;
        private readonly TicketGrpc.TicketGrpcClient _grpcClient;

        public TicketApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient(HttpClientName);
            _httpClient.BaseAddress = new Uri(configuration[UrlKey.TicketHttpApi]);

            var grpcChannel = GrpcChannel.ForAddress(configuration[UrlKey.TicketGrpcApi]);
            _grpcClient = new TicketGrpc.TicketGrpcClient(grpcChannel);
        }

        public async Task<bool> GetHealthStatus(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _httpClient.GetAsync("api/server", cancellationToken);

                return result.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<TicketItemDto> GetTicket(int id, CancellationToken cancellationToken)
        {
            return await _httpClient.GetFromJsonAsync<TicketItemDto>($"/api/tickets/{id}", cancellationToken);
        }

        public async Task UpdateDisplayText(UpdateDisplayTextDto updateDisplayTextDto, CancellationToken cancellationToken)
        {
            await _grpcClient.UpdateDisplayTextAsync(new UpdateDisplayTextGrpcDto
            {
                TicketId = updateDisplayTextDto.TicketId,
                LanguageCode = (int)updateDisplayTextDto.LanguageCode,
                DisplayText = updateDisplayTextDto.DislayText,
            }, cancellationToken: cancellationToken);
        }
    }
}
