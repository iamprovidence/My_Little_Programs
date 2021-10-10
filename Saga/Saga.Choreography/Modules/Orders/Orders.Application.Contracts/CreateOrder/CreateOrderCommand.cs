using Saga.Application.CQRS.Models;

namespace Orders.Application.Contracts.CreateOrder
{
	public record CreateOrderCommand : IdempotentCommand
	{
		public int OrderId { get; set; }
	}
}
