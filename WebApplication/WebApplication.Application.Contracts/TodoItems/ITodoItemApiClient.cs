using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Contracts.TodoItems.GetTodoItem;

namespace WebApplication.Application.Contracts.TodoItems
{
	public interface ITodoItemApiClient
	{
		Task<TodoItemViewModel> GetTodoItem(int todoItemId, CancellationToken cancellationToken = default);
	}
}
