using System.Threading;
using System.Threading.Tasks;
using WebArchitecture.Application.Contracts.TodoItems.GetTodoItem;

namespace WebArchitecture.Application.Contracts.TodoItems
{
	public interface ITodoItemApiClient
	{
		Task<TodoItemViewModel> GetTodoItem(int todoItemId, CancellationToken cancellationToken = default);
	}
}
