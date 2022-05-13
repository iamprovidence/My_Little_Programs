using DisplayTextWorker.Application;
using EventBus.Abstractions;
using TicketApi.Contracts.IntegrationEvents;

namespace DisplayTextWorker.Host.EventHandlers
{
    public class TicketCreatedEventHandler : IEventHandler<TicketCreatedIntegrationEvent>
    {
        private readonly DisplayTextAppService _appService;

        public TicketCreatedEventHandler(DisplayTextAppService appService)
        {
            _appService = appService;
        }

        public Task Handle(TicketCreatedIntegrationEvent @event)
        {
            return _appService.GenerateDisplayText(@event.TicketId, CancellationToken.None);
        }
    }
}
