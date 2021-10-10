using Messages.Application.Contracts.SendSms;
using Orders.Application.Contracts.CreateOrder;
using Orders.Application.Contracts.GetOrderById;
using Products.Application.Contracts.CreateProduct;
using Saga.Application.EventBus.Abstractions;
using System.Threading.Tasks;

namespace Messages.Application.SendSms
{
	public class SendSmsOnCreateProductSucceededIntegrationEvent : IIntegrationEventHandler<CreateProductSucceededIntegrationEvent>
	{
		private readonly IEventStore _eventStore;
		private readonly IEventBus _eventBus;

		public SendSmsOnCreateProductSucceededIntegrationEvent(IEventStore eventStore, IEventBus eventBus)
		{
			_eventStore = eventStore;
			_eventBus = eventBus;
		}

		public async Task Handle(CreateProductSucceededIntegrationEvent integrationEvent)
		{
			try
			{
				_eventBus.Publish(new SendSmsSucceededIntegrationEvent
				{
					CorrelationId = integrationEvent.CorrelationId,
				});

				var eventData = await _eventStore.SingleOrDefault<CreateOrderSucceededIntegrationEvent>(integrationEvent.CorrelationId);
				_eventBus.Publish(new CreateOrderResponseMessage
				{
					CorrelationId = integrationEvent.CorrelationId,
					OrderId = eventData.OrderId,
				});
			}
			catch
			{
				_eventBus.Publish(new SendSmsFailedIntegrationEvent
				{
					CorrelationId = integrationEvent.CorrelationId,
				});
			}
		}
	}
}
