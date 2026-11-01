using FlowStage.Interfaces;
using FlowStage.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopWizard.Application;

namespace ShopWizard
{
	internal static class Dependencies
	{
		public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IFlowStageOrchestrator, FlowStageOrchestrator>();
			services.AddScoped<IFlowStagePresenterOrchestrator, FlowStagePresenterOrchestrator>();

			services.Scan(scan => scan
				.FromAssemblyOf<IAssemblyMarker>()
				.AddClasses(c => c.AssignableTo<IFlowStart>())
				.AsImplementedInterfaces()
				.WithScopedLifetime());

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
