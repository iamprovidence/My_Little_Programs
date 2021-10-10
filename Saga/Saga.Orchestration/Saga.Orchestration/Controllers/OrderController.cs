using Microsoft.AspNetCore.Mvc;
using Orders.Application.Contracts.CreateOrder;
using Orders.Application.Contracts.GetOrderById;
using Saga.Gateway.Orders;
using System.Threading.Tasks;

namespace Saga.Orchestration.Controllers
{
	[ApiController]
	[Route("orders")]
	public class OrderController : ControllerBase
	{
		[HttpPost]
		public Task<OrderItem> CreateOrder([FromServices] CreateOrderSaga saga, [FromBody] CreateOrderCommand command)
		{
			return saga.Process(command);
		}
	}
}
