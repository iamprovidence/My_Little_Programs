using MediatR;
using Saga.Application.CQRS.Models;

namespace Saga.Application.CQRS.Abstractions
{
	public interface ICommandHandler<T> : IRequestHandler<T>
		where T : IdempotentCommand
	{
	}
}
