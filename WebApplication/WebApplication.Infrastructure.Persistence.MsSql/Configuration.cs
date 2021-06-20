using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication.Application.Persistence.Abstractions;
using WebApplication.Infrastructure.Persistence.MsSql.Services;

namespace WebApplication.Infrastructure.Persistence.MsSql
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
