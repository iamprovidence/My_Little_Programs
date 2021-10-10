using Saga.Application.EventBus.Models;

namespace Orders.Application.Contracts.GetOrderById
{
	public record CreateOrderResponseMessage : IntegrationEvent
	{
		public int OrderId { get; set; }
	}
}
