using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.ViewModels.TodoItems;

namespace WebApplication.Application.ApiClient.Abstractions
{
	public interface IWebApplicationClient
	{
		Task<TodoItemViewModel> GetTodoItem(int todoItemId, CancellationToken cancellationToken = default);
	}
}
