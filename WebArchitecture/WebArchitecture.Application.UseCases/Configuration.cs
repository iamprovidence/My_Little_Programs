using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebArchitecture.Application.UseCases
{
	public static class Configuration
	{
		public static IServiceCollection AddApplicationUseCases(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddMediatR(typeof(Configuration).Assembly);

			return services;
		}
	}
}
