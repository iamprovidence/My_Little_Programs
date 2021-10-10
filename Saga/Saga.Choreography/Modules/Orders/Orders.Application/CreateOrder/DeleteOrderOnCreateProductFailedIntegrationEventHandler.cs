using Products.Application.Contracts.CreateProduct;
using Saga.Application.EventBus.Abstractions;
using System.Threading.Tasks;

namespace Orders.Application.CreateOrder
{
	public class DeleteOrderOnCreateProductFailedIntegrationEventHandler : IIntegrationEventHandler<CreateProductFailedIntegrationEvent>
	{
		public Task Handle(CreateProductFailedIntegrationEvent integrationEvent)
		{
			// delete order

			return Task.CompletedTask;
		}
	}
}
