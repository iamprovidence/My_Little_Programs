using EventBus.Abstract;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
	public sealed class RabbitMqEventBus : IEventBus
	{
		private readonly Dictionary<string, List<Type>> _handlers;

		private readonly IMediator _mediator;
		private readonly IServiceScopeFactory _serviceScopeFactory;

		public RabbitMqEventBus(IMediator mediator, IServiceScopeFactory serviceScopeFactory)
		{
			_mediator = mediator;
			_serviceScopeFactory = serviceScopeFactory;
			_handlers = new Dictionary<string, List<Type>>();
		}

		public void Publish<T>(T @event) where T : IEvent
		{
			var factory = new ConnectionFactory() { HostName = "localhost" };
			using (var connection = factory.CreateConnection())
			using (var channel = connection.CreateModel())
			{
				var eventName = @event.GetType().Name;
				var message = JsonConvert.SerializeObject(@event);
				var body = Encoding.UTF8.GetBytes(message);

				channel.QueueDeclare(eventName, false, false, false, null);
				channel.BasicPublish("", eventName, null, body);
			}
		}

		public void Subscribe<T, TH>()
			where T : IEvent
			where TH : IEventHandler<T>
		{
			var eventName = typeof(T).Name;
			var handlerType = typeof(TH);

			if (!_handlers.ContainsKey(eventName))
			{
				_handlers.Add(eventName, new List<Type>());
			}

			if (_handlers[eventName].Any(s => s.GetType() == handlerType))
			{
				throw new ArgumentException($"Handler Type {handlerType.Name} already is registered for '{eventName}'", nameof(handlerType));
			}

			_handlers[eventName].Add(handlerType);

			StartBasicConsume<T>();
		}

		private void StartBasicConsume<T>() where T : IEvent
		{
			var factory = new ConnectionFactory()
			{
				HostName = "localhost",
				DispatchConsumersAsync = true
			};

			var connection = factory.CreateConnection();
			var channel = connection.CreateModel();

			var eventName = typeof(T).Name;

			channel.QueueDeclare(eventName, false, false, false, null);

			var consumer = new AsyncEventingBasicConsumer(channel);
			consumer.Received += Consumer_Received;

			channel.BasicConsume(eventName, true, consumer);
		}

		private async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
		{
			var eventName = e.RoutingKey;
			var message = Encoding.UTF8.GetString(e.Body.ToArray());

			await ProcessEvent(eventName, message).ConfigureAwait(false);
		}

		private async Task ProcessEvent(string eventName, string message)
		{
			if (!_handlers.ContainsKey(eventName)) return;

			using (var scope = _serviceScopeFactory.CreateScope())
			{
				foreach (var subscription in _handlers[eventName])
				{
					var handler = scope.ServiceProvider.GetService(subscription);
					if (handler == null) continue;

					var eventType = Type.GetType(eventName);
					var @event = JsonConvert.DeserializeObject(message, eventType);
					var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);
					var method = concreteType.GetMethod(nameof(IEventHandler<IEvent>.Handle));

					await (Task)method.Invoke(handler, new object[] { @event });
				}
			}
		}
	}
}
