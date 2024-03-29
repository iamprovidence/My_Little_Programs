﻿using MediatR;

namespace WebArchitecture.Application.ViewModels.TodoItems
{
	public record GetTodoItemQuery : IRequest<TodoItemViewModel>
	{
		public int TodoItemId { get; init; }
	}
}
