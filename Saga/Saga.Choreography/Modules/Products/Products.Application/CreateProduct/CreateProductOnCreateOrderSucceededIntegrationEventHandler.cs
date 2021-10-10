using Orders.Application.Contracts.CreateOrder;
using Products.Application.Contracts.CreateProduct;
using Saga.Application.EventBus.Abstractions;
using System.Threading.Tasks;

namespace Products.Application.CreateProduct
{
	public class CreateProductOnCreateOrderSucceededIntegrationEventHandler : IIntegrationEventHandler<CreateOrderSucceededIntegrationEvent>
	{
		private readonly IEventBus _eventBus;

		public CreateProductOnCreateOrderSucceededIntegrationEventHandler(IEventBus eventBus)
		{
			_eventBus = eventBus;
		}

		public Task Handle(CreateOrderSucceededIntegrationEvent integrationEvent)
		{
			try
			{
				_eventBus.Publish(new CreateProductSucceededIntegrationEvent
				{
					CorrelationId = integrationEvent.CorrelationId,
				});
			}
			catch
			{
				_eventBus.Publish(new CreateProductFailedIntegrationEvent
				{
					CorrelationId = integrationEvent.CorrelationId,
				});
			}

			return Task.CompletedTask;
		}
	}
}
