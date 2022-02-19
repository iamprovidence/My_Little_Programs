using Azure.Messaging.ServiceBus;
using EventBus.Abstract;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
	public class ServiceBusEventBus : IEventBus
	{
		private static readonly string TopicName = "secret-topic";

		private readonly ServiceBusSender _sender;
		private readonly ServiceBusProcessor _processor;

		private readonly IServiceScopeFactory _serviceScopeFactory;
		private readonly Dictionary<string, List<Type>> _handlers;

		public ServiceBusEventBus(IServiceScopeFactory serviceScopeFactory)
		{
			var serviceBusClient = new ServiceBusClient("localhost");

			_sender = serviceBusClient.CreateSender(TopicName);
			_processor = serviceBusClient.CreateProcessor(TopicName);

			_serviceScopeFactory = serviceScopeFactory;
			_handlers = new Dictionary<string, List<Type>>();

			RegisterMessageHandler();
		}

		private void RegisterMessageHandler()
		{
			_processor.ProcessMessageAsync += async (args) =>
			{
				var eventName = args.Message.Subject;
				var messageData = args.Message.Body.ToString();

				// Complete the message so that it is not received again.
				await ProcessEvent(eventName, messageData);

				await args.CompleteMessageAsync(args.Message);

			};

			_processor.StartProcessingAsync().Wait();
		}

		public void Publish<T>(T @event) where T : IEvent
		{
			var eventName = @event.GetType().Name;
			var jsonMessage = JsonConvert.SerializeObject(@event);
			var body = Encoding.UTF8.GetBytes(jsonMessage);

			var message = new ServiceBusMessage
			{
				MessageId = Guid.NewGuid().ToString(),
				Body = new BinaryData(body),
				Subject = eventName,
			};

			_sender.SendMessageAsync(message).Wait();
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
