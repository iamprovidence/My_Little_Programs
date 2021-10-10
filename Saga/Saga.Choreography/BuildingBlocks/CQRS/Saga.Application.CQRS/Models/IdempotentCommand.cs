using MediatR;

namespace Saga.Application.CQRS.Models
{
	public record IdempotentCommand : IRequest
	{
		public string CommandId { get; set; }
	}
}
