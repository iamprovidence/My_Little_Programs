using Microsoft.AspNetCore.Mvc;
using Orders.Application.Contracts.CreateOrder;
using Orders.Application.Contracts.GetOrderById;
using Saga.Application.CQRS.Abstractions;
using System.Threading.Tasks;

namespace Saga.Choreography.Controllers
{
	[ApiController]
	[Route("orders")]
	public class OrderController : ControllerBase
	{
		private readonly IMessageDispatcher _dispatcher;

		public OrderController(IMessageDispatcher dispatcher)
		{
			_dispatcher = dispatcher;
		}

		[HttpPost]
		public Task<CreateOrderResponseMessage> CreateOrder([FromBody] CreateOrderCommand command)
		{
			return _dispatcher.Dispatch<CreateOrderCommand, CreateOrderResponseMessage>(command);
		}
	}
}
