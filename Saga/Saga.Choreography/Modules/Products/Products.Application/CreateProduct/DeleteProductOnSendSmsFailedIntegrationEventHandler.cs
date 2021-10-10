using Messages.Application.Contracts.SendSms;
using Products.Application.Contracts.CreateProduct;
using Saga.Application.EventBus.Abstractions;
using System.Threading.Tasks;

namespace Products.Application.CreateProduct
{
	public class DeleteProductOnSendSmsFailedIntegrationEventHandler : IIntegrationEventHandler<SendSmsFailedIntegrationEvent>
	{
		private readonly IEventBus _eventBus;

		public DeleteProductOnSendSmsFailedIntegrationEventHandler(IEventBus eventBus)
		{
			_eventBus = eventBus;
		}

		public Task Handle(SendSmsFailedIntegrationEvent integrationEvent)
		{
			try
			{
				_eventBus.Publish(new CreateProductFailedIntegrationEvent
				{
					CorrelationId = integrationEvent.CorrelationId,
				});
			}
			catch
			{
				// Log exception
				// Compensation handlers should not produce new exception messages because it can lead to infinite loop
			}

			return Task.CompletedTask;
		}
	}
}
