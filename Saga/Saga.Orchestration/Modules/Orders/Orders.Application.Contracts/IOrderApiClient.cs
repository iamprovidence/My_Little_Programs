using Orders.Application.Contracts.CreateOrder;
using Orders.Application.Contracts.DeleteOrder;
using Orders.Application.Contracts.GetOrderById;

namespace Orders.Application.Contracts
{
	public interface IOrderApiClient
	{
		void Execute(CreateOrderCommand command);
		void Execute(DeleteOrderCommand command);
		OrderItem Query(GetOrderByIdQuery query);
	}
}
