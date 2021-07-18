using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Northwind.Infrastructure.Persistence;

namespace Northwind.Api
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services
				.AddDbContextPool<NorthwindDbContext>(options =>
				{
					options.UseSqlServer("Server=127.0.0.1,5533;Database=Northwind.Db;User Id=sa;Password=Pass@word");
				});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGet("/", async context =>
				{
					await context.Response.WriteAsync("Hello World!");
				});
			});
		}
	}
}
