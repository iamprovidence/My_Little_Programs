using EventBus.Abstract;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventBus
{
	public sealed class InProcessEventBus : IEventBus
	{
		private class Message
		{
			public string EventName { get; set; }
			public string Payload { get; set; }
		}

		private static readonly string PipeName = "secret-pipe";

		private readonly Dictionary<string, List<Type>> _handlers;
		private readonly IServiceScopeFactory _serviceScopeFactory;
		private readonly bool _isConsuming;

		public InProcessEventBus(IServiceScopeFactory serviceScopeFactory)
		{
			_serviceScopeFactory = serviceScopeFactory;
			_handlers = new Dictionary<string, List<Type>>();
			_isConsuming = false;
		}

		public void Publish<T>(T @event) where T : IEvent
		{
			using (var serverPipeStream = new NamedPipeServerStream(PipeName, PipeDirection.InOut, maxNumberOfServerInstances: 1, PipeTransmissionMode.Message))
			using (var streamWriter = new StreamWriter(serverPipeStream))
			{
				serverPipeStream.WaitForConnection();

				var message = new Message
				{
					EventName = typeof(T).Name,
					Payload = JsonConvert.SerializeObject(@event),
				};
				var value = JsonConvert.SerializeObject(message);

				streamWriter.WriteLine(value);
				streamWriter.Flush();
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

			TryStartConsume<T>();
		}

		private void TryStartConsume<T>()
			where T : IEvent
		{
			if (_isConsuming)
			{
				return;
			}

			ThreadPool.QueueUserWorkItem((state) =>
			{
				using (var clientPipeStream = new NamedPipeClientStream(PipeName))
				using (var streamReader = new StreamReader(clientPipeStream))
				{
					clientPipeStream.Connect();
					clientPipeStream.ReadMode = PipeTransmissionMode.Message;

					while (true)
					{
						var value = streamReader.ReadLine();
						var message = JsonConvert.DeserializeObject<Message>(value);

						ProcessEvent(message.EventName, message.Payload);

						Thread.Sleep(1000);
					}
				}
			});
		}

		private void ProcessEvent(string eventName, string message)
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

					((Task)method.Invoke(handler, new object[] { @event })).Wait();
				}
			}
		}
	}
}
