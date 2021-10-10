using MediatR;
using Saga.Application.CQRS.Abstractions;
using Saga.Application.CQRS.Models;
using Saga.Application.EventBus.Abstractions;
using Saga.Application.EventBus.Models;
using System;
using System.Threading.Tasks;

namespace Saga.Application.CQRS.Services
{
	public class MessageDispatcher : IMessageDispatcher, IIntegrationEventHandler<IntegrationEvent>
	{
		private readonly IMediator _mediator;
		private readonly IRequestManager _requestManager;
		private readonly IPendingTaskStore _pendingTaskStore;

		public MessageDispatcher(
			IMediator mediator,
			IRequestManager requestManager,
			IPendingTaskStore pendingTaskStore)
		{
			_mediator = mediator;
			_requestManager = requestManager;
			_pendingTaskStore = pendingTaskStore;
		}

		public async Task<TResponse> Dispatch<TRequest, TResponse>(TRequest command)
			where TRequest : IdempotentCommand
		{
			_requestManager.Validate(command);

			var pendingTask = _pendingTaskStore.GetOrAdd<TResponse>(taskId: command.CommandId);

			await _mediator.Send(command);

			return await pendingTask;
		}

		Task IIntegrationEventHandler<IntegrationEvent>.Handle(IntegrationEvent integrationEvent)
		{
			TryCompletePendingCommand(integrationEvent);

			return Task.CompletedTask;
		}

		private void TryCompletePendingCommand(IntegrationEvent integrationEvent)
		{
			var eventType = integrationEvent.GetType();

			var methodInfo = typeof(IPendingTaskStore)
				.GetMethod(nameof(IPendingTaskStore.TryComplete))
				.MakeGenericMethod(eventType);

			methodInfo
				.Invoke(_pendingTaskStore, new[] { integrationEvent.CorrelationId, Convert.ChangeType(integrationEvent, eventType) });
		}
	}
}
