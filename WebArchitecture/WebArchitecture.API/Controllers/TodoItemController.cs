using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using WebArchitecture.Application.Shared.Models;
using WebArchitecture.Application.ViewModels.TodoItems;

namespace WebArchitecture.API.Controllers
{
	[ApiController]
	[Route("api/todo-items")]
	public class TodoItemController : ControllerBase
	{
		private readonly ISender _sender;

		public TodoItemController(ISender sender)
		{
			_sender = sender;
		}

		[HttpGet("{todoItemId}")]
		[ProducesResponseType(typeof(WebAppResult<TodoItemViewModel>), (int)HttpStatusCode.OK)]
		// [ProducesResponseType(typeof(WebAppResult<TodoItemViewModel>), StatusCodes.Status200OK)]
		public Task<TodoItemViewModel> GetTodoItem(int todoItemId, CancellationToken cancellationToken)
		{
			var request = new GetTodoItemQuery()
			{
				TodoItemId = todoItemId,
			};

			return _sender.Send(request, cancellationToken);
		}
	}
}
