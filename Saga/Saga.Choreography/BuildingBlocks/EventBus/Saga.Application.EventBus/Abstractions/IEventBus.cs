using Saga.Application.EventBus.Models;

namespace Saga.Application.EventBus.Abstractions
{
	public interface IEventBus
	{
		public void Publish<T>(T integrationEvent)
			where T : IntegrationEvent;

		void Subscribe<T, TH>()
			where T : IntegrationEvent
			where TH : IIntegrationEventHandler<T>;
	}
}
