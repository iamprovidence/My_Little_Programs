using EventBus.Abstractions;

namespace TicketApi.Contracts.IntegrationEvents
{
    public class TicketCreatedIntegrationEvent : IEvent
    {
        public int TicketId { get; init; }
    }
}
