using Microsoft.Extensions.DependencyInjection;
using Saga.Application.EventBus.Abstractions;
using Saga.Application.EventBus.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Saga.Infrastructure.InMemoryEventBus
{
	public sealed class InMemoryEventBus : IEventBus
	{
		private readonly IEventStore _eventStore;
		private readonly ICollection<Type> _handlers;
		private readonly IServiceScopeFactory _serviceScopeFactory;

		public InMemoryEventBus(IEventStore eventStore, IServiceScopeFactory serviceScopeFactory)
		{
			_eventStore = eventStore;
			_serviceScopeFactory = serviceScopeFactory;
			_handlers = new List<Type>();
		}

		public void Subscribe<T, TH>()
			where T : IntegrationEvent
			where TH : IIntegrationEventHandler<T>
		{
			var eventName = typeof(T).Name;
			var handlerType = typeof(TH);

			if (_handlers.Contains(handlerType))
			{
				throw new ArgumentException($"Handler Type {handlerType.Name} already is registered for '{eventName}'", nameof(handlerType));
			}

			_handlers.Add(handlerType);
		}

		public void Publish<TEvent>(TEvent integrationEvent)
			where TEvent : IntegrationEvent
		{
			_eventStore.Add(integrationEvent);

			foreach (var handler in _handlers.Where(IsEventHandler<TEvent>).Select(CreateHandlerFromType<TEvent>).Where(handler => handler is not null))
			{
				handler.Handle(integrationEvent);
			}
		}

		private bool IsEventHandler<TEvent>(Type handlerType)
			where TEvent : IntegrationEvent
		{
			return typeof(IIntegrationEventHandler<TEvent>).IsAssignableFrom(handlerType);
		}

		private IIntegrationEventHandler<TEvent> CreateHandlerFromType<TEvent>(Type handlerType)
			where TEvent : IntegrationEvent
		{
			using var serviceScope = _serviceScopeFactory.CreateScope();

			return serviceScope.ServiceProvider.GetService(handlerType) as IIntegrationEventHandler<TEvent>;
		}

	}
}
