using System.Threading;
using System.Threading.Tasks;
using WebArchitecture.Application.Contracts;
using WebArchitecture.Application.Contracts.TodoItems.GetTodoItem;

namespace WebArchitecture.Application.Contracts.HttpClient
{
	internal class WebArchitectureHttpClient : IWebArchitectureApiClient
	{
		public Task<TodoItemViewModel> GetTodoItem(int todoItemId, CancellationToken cancellationToken = default)
		{
			throw new System.NotImplementedException();
		}
	}
}
