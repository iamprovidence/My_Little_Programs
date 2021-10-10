using Saga.Application.EventBus.Models;

namespace Products.Application.Contracts.CreateProduct
{
	public record CreateProductFailedIntegrationEvent : IntegrationEvent
	{
	}
}
