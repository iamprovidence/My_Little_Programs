using Saga.Application.CQRS.Models;
using System.Threading.Tasks;

namespace Saga.Application.CQRS.Abstractions
{
	public interface IMessageDispatcher
	{
		Task<TResponse> Dispatch<TRequest, TResponse>(TRequest command)
			where TRequest : IdempotentCommand;
	}
}
