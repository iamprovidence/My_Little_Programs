using DaCache.Abstractions.Interfaces;
using DaCache.Implementation.Interfaces;
using DaCache.Implementation.Services;
using DaCache.Implementation.Telemetry;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace DaCache.Implementation
{
	public static class ServiceCollectionConfiguration
	{
		public static void AddCacheServices(this IServiceCollection services, IConfiguration configuration, string connectionString)
		{
			services.AddMemoryCache();

			var trackingOptions = new TrackingOptions
			{
				TrackingDependencyName = "DaCache",
				TrackTarget = connectionString,
			};

			services.AddSingleton<BinarySerializer>();
			services.AddSingleton<ISerializer>(serviceProvider =>
			{
				var serializer = serviceProvider.GetRequiredService<BinarySerializer>();
				var telemetryClient = serviceProvider.GetRequiredService<TelemetryClient>();

				return new SerializerTrackingDecorator(serializer, telemetryClient, trackingOptions);
			});

			services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(configuration.GetConnectionString(connectionString)));

			services.AddScoped<CacheService>();
			services.AddScoped<ICacheService>(serviceProvider =>
			{
				var redisCache = serviceProvider.GetRequiredService<CacheService>();
				var telemetryClient = serviceProvider.GetRequiredService<TelemetryClient>();

				return new CacheServiceTrackingDecorator(redisCache, telemetryClient, trackingOptions);
			});

			services.AddDistributedRedisCache(option =>
			{
				option.Configuration = configuration.GetConnectionString(connectionString);
				option.InstanceName = "master";
			});
		}
	}
}
