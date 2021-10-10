using Saga.Application.CQRS.Abstractions;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Saga.Application.CQRS.Services
{
	public class PendingTaskStore : IPendingTaskStore
	{
		private readonly ConcurrentDictionary<string, object> _waitingTasks = new ConcurrentDictionary<string, object>();

		public Task<TResult> GetOrAdd<TResult>(string taskId)
		{
			var taskSource = _waitingTasks.GetOrAdd(taskId, new TaskCompletionSource<TResult>());

			return ((TaskCompletionSource<TResult>)taskSource).Task;
		}

		public bool TryComplete<TResult>(string taskId, TResult result)
		{
			if (!_waitingTasks.ContainsKey(taskId))
			{
				return false;
			}

			if (_waitingTasks[taskId] is not TaskCompletionSource<TResult>)
			{
				return false;
			}

			if (!_waitingTasks.TryRemove(taskId, out var waitingTask))
			{
				return false;
			}

			var taskSource = (TaskCompletionSource<TResult>)waitingTask;

			taskSource.SetResult(result);

			return true;
		}
	}
}
