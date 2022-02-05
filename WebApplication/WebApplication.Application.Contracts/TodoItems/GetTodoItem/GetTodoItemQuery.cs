﻿using MediatR;

namespace WebApplication.Application.Contracts.TodoItems.GetTodoItem
{
	public record GetTodoItemQuery : IRequest<TodoItemViewModel>
	{
		public int TodoItemId { get; init; }
	}
}
