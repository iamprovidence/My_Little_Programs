using System.Text;
using EventBus.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EventBus.RabbitMq
{
    internal sealed class RabbitMqEventBus : IEventBus
    {
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConfiguration _configuration;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public RabbitMqEventBus(
            IServiceScopeFactory serviceScopeFactory,
            IConfiguration configuration,
            IHostApplicationLifetime hostApplicationLifetime)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = configuration;
            _hostApplicationLifetime = hostApplicationLifetime;
            _handlers = new Dictionary<string, List<Type>>();
        }

        public void Publish<T>(T @event) where T : IEvent
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["ConnectionStrings:RabbitMqHost"],
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var eventName = @event.GetType().AssemblyQualifiedName;
                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                channel.QueueDeclare(queue: eventName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                channel.BasicPublish(exchange: "", routingKey: eventName, basicProperties: null, body);
            }
        }

        public void Subscribe<T, TH>()
            where T : IEvent
            where TH : IEventHandler<T>
        {
            var eventName = typeof(T).AssemblyQualifiedName;
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
            // wait when application configured
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-3.1&tabs=visual-studio#startasync-1
            // DelayWithoutException(Timeout.Infinite, _hostApplicationLifetime.ApplicationStarted).Wait();

            var factory = new ConnectionFactory()
            {
                HostName = _configuration["ConnectionStrings:RabbitMqHost"],
                DispatchConsumersAsync = true,
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            var eventName = typeof(T).AssemblyQualifiedName;

            channel.QueueDeclare(queue: eventName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;

            channel.BasicConsume(queue: eventName, autoAck: true, consumer);
        }

        private Task DelayWithoutException(int infinite, CancellationToken applicationStarted)
        {
            return Task.WhenAny(Task.Delay(infinite, applicationStarted));
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
