using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using WebApplication.Infrastructure.OpenApi.Swagger.SchemaFilters;

namespace WebApplication.Infrastructure.OpenApi.Swagger
{
	public static class Configuration
	{
		public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration, Action<OpenApiInfo> configureOpenApiInfo)
		{
			services.AddSwaggerGen(c =>
			{
				var openApiInfo = new OpenApiInfo();
				configureOpenApiInfo.Invoke(openApiInfo);

				c.SwaggerDoc("v1", openApiInfo);
				c.SchemaFilter<EnumSchemaFilter>();
			});

			return services;
		}

		public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication v1");
			});

			return app;
		}
	}
}
