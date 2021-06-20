using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;
using System;
using System.Net;
using System.Net.Http;
using WebApplication.Application.ApiClient.Abstractions;
using WebApplication.Infrastructure.ApiClient.Http.Services;

namespace WebApplication.Infrastructure.ApiClient.Http
{
	public static class Configuration
	{
		public static IServiceCollection AddWebApplicationHttpClient(this IServiceCollection services, IConfiguration configuration, string serviceUrlKey)
		{
			services
				.AddHttpClient<IWebApplicationClient, WebApplicationHttpClient>(client =>
				{
					var serviceUrl = configuration[serviceUrlKey];

					client.BaseAddress = new Uri(serviceUrl);
					client.DefaultRequestHeaders.Add("Accept", "application/json");
				})
				.AddPolicyHandler(BuildRetryPolicy())
				.AddPolicyHandler(BuildCircuitBreakerPolicy());


			return services;
		}

		private static IAsyncPolicy<HttpResponseMessage> BuildRetryPolicy()
		{
			return HttpPolicyExtensions
				.HandleTransientHttpError()
					.WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt) / 2));
		}

		private static IAsyncPolicy<HttpResponseMessage> BuildCircuitBreakerPolicy()
		{
			return HttpPolicyExtensions
				.HandleTransientHttpError()
				.Or<TimeoutRejectedException>()
				.OrResult(r => r.StatusCode == HttpStatusCode.TooManyRequests)
					.CircuitBreakerAsync(10, TimeSpan.FromMinutes(1));
		}
	}
}
