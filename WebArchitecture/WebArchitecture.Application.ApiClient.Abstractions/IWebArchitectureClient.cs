using System.Threading;
using System.Threading.Tasks;
using WebArchitecture.Application.ViewModels.TodoItems;

namespace WebArchitecture.Application.ApiClient.Abstractions
{
	public interface IWebArchitectureClient
	{
		Task<TodoItemViewModel> GetTodoItem(int todoItemId, CancellationToken cancellationToken = default);
	}
}
