using MediatR;
using Orders.Application.Contracts.CreateOrder;
using Saga.Application.CQRS.Abstractions;
using Saga.Application.EventBus.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace Orders.Application
{
	public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
	{
		private readonly IEventBus _eventBus;

		public CreateOrderCommandHandler(IEventBus eventBus)
		{
			_eventBus = eventBus;
		}

		public Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			try
			{
				// logic

				_eventBus.Publish(new CreateOrderSucceededIntegrationEvent
				{
					CorrelationId = request.CommandId,
					OrderId = request.OrderId,
				});
			}
			catch
			{
				_eventBus.Publish(new CreateOrderFailedIntegrationEvent
				{
					CorrelationId = request.CommandId,
				});
			}

			return Unit.Task;
		}
	}
}
