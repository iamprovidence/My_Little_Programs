using Saga.Application.EventBus.Models;
using System.Threading.Tasks;

namespace Saga.Application.EventBus.Abstractions
{
	public interface IEventStore
	{
		Task Add<TEvent>(TEvent integrationEvent)
			where TEvent : IntegrationEvent;
		Task<TEvent> SingleOrDefault<TEvent>(string correlationId)
			where TEvent : IntegrationEvent;
	}
}
