using EventBus.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus.RabbitMq
{
    public static class RabbitMqConfiguration
    {
        public static IServiceCollection AddRabbitMqEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEventBus, RabbitMqEventBus>();

            return services;
        }
    }
}
