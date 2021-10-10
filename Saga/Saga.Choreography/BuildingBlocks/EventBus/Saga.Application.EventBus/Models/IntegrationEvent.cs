namespace Saga.Application.EventBus.Models
{
	public record IntegrationEvent
	{
		public string CorrelationId { get; set; }
	}
}
