using Saga.Application.EventBus.Models;

namespace Orders.Application.Contracts.CreateOrder
{
	public record CreateOrderSucceededIntegrationEvent : IntegrationEvent
	{
		public int OrderId { get; set; }
	}
}
