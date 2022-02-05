using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WebApplication.Application.Contracts
{
	public static class Configuration
	{
		public static IServiceCollection AddWebApplicationApiClient(this IServiceCollection services, IConfiguration configuration, string serviceUrlKey)
		{
			throw new NotImplementedException();
		}
	}
}
