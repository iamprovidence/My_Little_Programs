using Saga.Application.EventBus.Models;

namespace Products.Application.Contracts.DeleteProduct
{
	public record ProductDeletedIntegrationEvent : IntegrationEvent
	{
	}
}
