using Saga.Application.EventBus.Models;

namespace Orders.Application.Contracts.CreateOrder
{
	public record CreateOrderFailedIntegrationEvent : IntegrationEvent
	{
	}
}
