using Saga.Application.EventBus.Events;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Saga.Gateway
{
	public class WaitingTaskStore : IWaitingTaskStore
	{
		private readonly ConcurrentDictionary<string, object> _waitingTasks = new ConcurrentDictionary<string, object>();

		public Task<TMessage> Add<TMessage>(string correlationId) where TMessage : IntegrationEvent
		{
			var tcs = _waitingTasks.GetOrAdd(correlationId, new TaskCompletionSource<TMessage>());

			return ((TaskCompletionSource<TMessage>)tcs).Task;
		}

		public bool TryComplete<TEvent>(TEvent integrationEvent) where TEvent : IntegrationEvent
		{
			if (!_waitingTasks.TryRemove(integrationEvent.CorrelationId, out var obj))
			{
				return false;
			}

			var tcs = (TaskCompletionSource<TEvent>)obj;

			tcs.SetResult(integrationEvent);

			return true;
		}
	}
}
