using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Identity.Abstractions;
using WebApplication.Application.Persistence.Abstractions;
using WebApplication.Application.Shared.Exceptions;
using WebApplication.Application.ViewModels.TodoItems;
using WebApplication.Domain.Entities;

namespace WebApplication.Application.UseCases.TodoItems
{
	internal class GetTodoItemQueryHandler : IRequestHandler<GetTodoItemQuery, TodoItemViewModel>
	{
		private readonly IApplicationDbContext _dbContext;
		private readonly ICurrentUserService _currentUserService;

		public GetTodoItemQueryHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
		{
			_dbContext = dbContext;
			_currentUserService = currentUserService;
		}

		public async Task<TodoItemViewModel> Handle(GetTodoItemQuery request, CancellationToken cancellationToken)
		{
			var todoItemDescription = await _dbContext
				.GetDbSet<TodoItem>()
				.Where(x => x.Id == request.TodoItemId)
				.Select(x => x.Description)
				.SingleOrDefaultAsync(cancellationToken);

			if (todoItemDescription is null)
			{
				throw new WebAppApplicationException(WebAppErrorCode.EntityNotFound, $"Todo item with id#{request.TodoItemId} has not been found");
			}

			var userName = await _currentUserService
				.GetUserName(cancellationToken);

			return new TodoItemViewModel
			{
				Description = todoItemDescription,
				UserName = userName,
			};
		}
	}
}
