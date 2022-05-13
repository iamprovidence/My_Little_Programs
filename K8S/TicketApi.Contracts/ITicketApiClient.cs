using TicketApi.Contracts.ViewModels;

namespace TicketApi.Contracts
{
    public interface ITicketApiClient
    {
        Task<bool> GetHealthStatus(CancellationToken cancellationToken);
        Task<TicketItemDto> GetTicket(int id, CancellationToken cancellationToken);
        Task UpdateDisplayText(UpdateDisplayTextDto updateDisplayTextDto, CancellationToken cancellationToken);
    }
}
