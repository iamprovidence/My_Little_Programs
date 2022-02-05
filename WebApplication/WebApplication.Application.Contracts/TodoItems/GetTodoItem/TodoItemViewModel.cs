namespace WebApplication.Application.Contracts.TodoItems.GetTodoItem
{
	public record TodoItemViewModel
	{
		public string Description { get; init; }
		public string UserName { get; init; }
	}
}
