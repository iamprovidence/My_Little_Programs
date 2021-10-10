using Saga.Application.EventBus.Models;

namespace Messages.Application.Contracts.SendSms
{
	public record SendSmsFailedIntegrationEvent : IntegrationEvent
	{
	}
}
