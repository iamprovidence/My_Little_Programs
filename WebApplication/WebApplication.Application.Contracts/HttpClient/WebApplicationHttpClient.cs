using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Contracts.TodoItems.GetTodoItem;

namespace WebApplication.Application.Contracts.HttpClient
{
	internal class WebApplicationHttpClient : IWebApplicationApiClient
	{
		public Task<TodoItemViewModel> GetTodoItem(int todoItemId, CancellationToken cancellationToken = default)
		{
			throw new System.NotImplementedException();
		}
	}
}
