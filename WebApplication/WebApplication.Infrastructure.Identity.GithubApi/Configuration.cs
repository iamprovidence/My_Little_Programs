using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using WebApplication.Application.Identity.Abstractions;
using WebApplication.Infrastructure.Identity.GitHubApi.Interfaces;
using WebApplication.Infrastructure.Identity.GitHubApi.Services;

namespace WebApplication.Infrastructure.Identity.GitHubApi
{
	public static class Configuration
	{
		public static IServiceCollection AddGitHubIdentity(this IServiceCollection services, IConfiguration configuration, string serviceUrlKey)
		{
			services
				.AddRefitClient<IGitHubApiHttpClient>()
				.ConfigureHttpClient(c =>
				{
					var serviceUrl = configuration[serviceUrlKey];
					c.BaseAddress = new Uri(serviceUrl);
					c.DefaultRequestHeaders.Add("User-Agent", "request");
				});

			services.AddScoped<ICurrentUserService, CurrentUserService>();

			return services;
		}
	}
}
