using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebArchitecture.Application.Persistence.Abstractions;
using WebArchitecture.Infrastructure.Persistence.MsSql.Services;

namespace WebArchitecture.Infrastructure.Persistence.MsSql
{
	public static class Configuration
	{
		public static IServiceCollection AddMsSqlPersistence(this IServiceCollection services, IConfiguration configuration, string connectionString)
		{
			services.AddDbContextPool<IApplicationDbContext, ApplicationDbContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString(connectionString));
			});

			return services;
		}
	}
}
