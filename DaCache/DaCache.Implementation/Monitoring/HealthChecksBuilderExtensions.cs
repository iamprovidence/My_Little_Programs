using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DaCache.Implementation.Monitoring
{
	public static class HealthChecksBuilderExtensions
	{
		public static void AddHealthCheckForRedis(this IHealthChecksBuilder healthCheckBuilder, IConfiguration configuration, string connectionString)
		{
			healthCheckBuilder.AddRedis(
				redisConnectionString: configuration.GetConnectionString(connectionString),
				name: $"Redis {connectionString} check",
				failureStatus: HealthStatus.Degraded,
				tags: new string[] { "redis", connectionString });
		}
	}
}
