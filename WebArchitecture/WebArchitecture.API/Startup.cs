using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebArchitecture.API.WebHostConfigurations;
using WebArchitecture.Application.Shared.Constants;
using WebArchitecture.Application.UseCases;
using WebArchitecture.Infrastructure.ApiClient.Http;
using WebArchitecture.Infrastructure.Identity.GitHubApi;
using WebArchitecture.Infrastructure.OpenApi.Swagger;
using WebArchitecture.Infrastructure.Persistence.MsSql;

namespace WebArchitecture.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers(options =>
			{
				options.Filters.Add(typeof(ResultWrapperFilter));
			});

			services.AddApplicationUseCases(Configuration);

			services.AddMsSqlPersistence(Configuration, ConnectionStringKeys.WebAppDb);
			services.AddWebApplicationHttpClient(Configuration, UrlKeys.WebApplicationApi);
			services.AddGitHubIdentity(Configuration, UrlKeys.GitHubApi);

			services.AddSwagger(Configuration, info =>
			{
				info.Title = "WebApplication";
				info.Version = "v1";
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();

				app.UseSwagger(env);
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
