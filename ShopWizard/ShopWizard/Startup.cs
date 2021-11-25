using FlowStage.Interfaces;
using FlowStage.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopWizard.Application;

namespace ShopWizard
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
			services.AddControllersWithViews();

			services.AddApplicationServices(Configuration);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}

	internal static class Dependencies
	{
		public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IFlowStageOrchestrator, FlowStageOrchestrator>();
			services.AddScoped<IFlowStagePresenterOrchestrator, FlowStagePresenterOrchestrator>();

			services.Scan(scan => scan
				.FromAssemblyOf<IAssemblyMarker>()
				.AddClasses(c => c.AssignableTo<IFlowStage>())
				.AsImplementedInterfaces()
				.WithScopedLifetime());

			services.Scan(scan => scan
				.FromAssemblyOf<IAssemblyMarker>()
				.AddClasses(c => c.AssignableTo<IFlowStagePresenter>())
				.AsImplementedInterfaces()
				.WithScopedLifetime());
		}
	}
}
