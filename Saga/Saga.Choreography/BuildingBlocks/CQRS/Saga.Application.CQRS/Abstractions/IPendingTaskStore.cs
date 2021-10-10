using System.Threading.Tasks;

namespace Saga.Application.CQRS.Abstractions
{
	public interface IPendingTaskStore
	{
		Task<TResult> GetOrAdd<TResult>(string taskId);
		bool TryComplete<TResult>(string taskId, TResult result);
	}
}
