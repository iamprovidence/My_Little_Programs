using Saga.Application.CQRS.Models;

namespace Saga.Application.CQRS.Abstractions
{
	public interface IRequestManager
	{
		void Validate<TRequest>(TRequest command)
			where TRequest : IdempotentCommand;
	}
}
