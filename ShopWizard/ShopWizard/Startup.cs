using FlowStage;
using FlowStage.Abstractions.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using ShopWizard.Application.CancelOrder;
using ShopWizard.Application.CancelOrder.Interfaces;
using ShopWizard.Application.CancelOrder.Stages;
using ShopWizard.Application.CreateOrder;
using ShopWizard.Application.CreateOrder.Interfaces;
using ShopWizard.Application.CreateOrder.Stages;
using ShopWizard.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

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
			services
				.AddControllersWithViews()
				.AddControllersAsServices(ServiceLifetime.Scoped);

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
			services.AddScoped<IOutputPortFactory, OutputPortFactory>();

			services.AddScoped<CreateOrderFlowService>();

			services.AddScoped<ICreateOrderFlowStage, ProductSelectionFlowStage>();
			services.AddScoped<ICreateOrderFlowStage, ContactDetailsFlowStage>();
			services.AddScoped<ICreateOrderFlowStage, PaymentDetailsFlowStage>();

			services.AddScoped<ICreateOrderAppService, CreateOrderAppService>();


			services.AddScoped<CancelOrderFlowService>(sp =>
			{
				var stages = new Lazy<IEnumerable<ICancelOrderFlowStage>>(() => sp.GetRequiredService<IEnumerable<ICancelOrderFlowStage>>());

				return new CancelOrderFlowService(stages);
			});

			services.AddScoped<ICancelOrderOutputPort>(sp => sp.GetRequiredService<CancelOrderController>());

			services.AddScoped<ICancelOrderFlowStage, EnterOrderCodeFlowStage>();
			services.AddScoped<ICancelOrderFlowStage, ConfirmCancelFlowStage>();

			services.AddScoped<ICancelOrderAppService, CancelOrderAppService>();
		}

		public static IMvcBuilder AddControllersAsServices(this IMvcBuilder builder, ServiceLifetime lifetime)
		{
			var feature = new ControllerFeature();
			builder.PartManager.PopulateFeature(feature);

			foreach (var controller in feature.Controllers.Select(c => c.AsType()))
			{
				builder.Services.Add(ServiceDescriptor.Describe(controller, controller, lifetime));
			}

			builder.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

			return builder;
		}
	}
}
