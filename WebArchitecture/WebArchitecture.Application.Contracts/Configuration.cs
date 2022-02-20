using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WebArchitecture.Application.Contracts
{
	public static class Configuration
	{
		public static IServiceCollection AddWebApplicationApiClient(this IServiceCollection services, IConfiguration configuration, string serviceUrlKey)
		{
			throw new NotImplementedException();
		}
	}
}
