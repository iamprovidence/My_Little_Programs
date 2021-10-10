using Saga.Application.EventBus.Models;
using System.Threading.Tasks;

namespace Saga.Application.EventBus.Abstractions
{
	public interface IIntegrationEventHandler<in TIntegrationEvent>
		where TIntegrationEvent : IntegrationEvent
	{
		Task Handle(TIntegrationEvent integrationEvent);
	}
}
