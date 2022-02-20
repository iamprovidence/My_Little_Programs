namespace WebArchitecture.Application.ViewModels.TodoItems
{
	public record TodoItemViewModel
	{
		public string Description { get; init; }
		public string UserName { get; init; }
	}
}
