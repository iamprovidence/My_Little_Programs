using Orders.Application.Contracts.CreateOrder;
using Orders.Application.Contracts.GetOrderById;
using Saga.Application.EventBus.Abstractions;
using System.Threading.Tasks;

namespace Orders.Application.CreateOrder
{
	public class CreateOrderFailedIntegrationEventHandler : IIntegrationEventHandler<CreateOrderFailedIntegrationEvent>
	{
		private readonly IEventBus _eventBus;

		public CreateOrderFailedIntegrationEventHandler(IEventBus eventBus)
		{
			_eventBus = eventBus;
		}

		public Task Handle(CreateOrderFailedIntegrationEvent integrationEvent)
		{
			_eventBus.Publish(new CreateOrderResponseMessage());

			return Task.CompletedTask;
		}
	}
}
