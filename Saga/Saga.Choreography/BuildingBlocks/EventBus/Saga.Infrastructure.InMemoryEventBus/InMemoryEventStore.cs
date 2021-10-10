using Saga.Application.EventBus.Abstractions;
using Saga.Application.EventBus.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saga.Infrastructure.InMemoryEventBus
{
	public class InMemoryEventStore : IEventStore
	{
		private readonly List<IntegrationEvent> _events = new List<IntegrationEvent>();

		public Task Add<TEvent>(TEvent integrationEvent)
			where TEvent : IntegrationEvent
		{
			_events.Add(integrationEvent);

			return Task.CompletedTask;
		}

		public Task<TEvent> SingleOrDefault<TEvent>(string correlationId)
			where TEvent : IntegrationEvent
		{
			var result = _events
				.OfType<TEvent>()
				.SingleOrDefault(x => x.CorrelationId == correlationId);

			return Task.FromResult(result);
		}
	}
}
