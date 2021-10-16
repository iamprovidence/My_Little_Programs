using FlowStage.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowStage.Services
{
	public class FlowStageOrchestrator : IFlowStageOrchestrator
	{
		private readonly IEnumerable<IFlowStage> _stageServices;

		public FlowStageOrchestrator(IEnumerable<IFlowStage> stageServices)
		{
			_stageServices = stageServices;
		}

		public async Task ProcessCommand<TFlowContext>(TFlowContext context, IFlowCommand command)
			where TFlowContext : IFlowContextChangeable
		{
			context.Reset();

			var stageService = GetFlowStageService<TFlowContext>(context);
			var nextStageName = await stageService.ProcessCommand(context, command);

			context.SetCurrentStage(nextStageName);
		}

		private IFlowStage<TFlowContext> GetFlowStageService<TFlowContext>(IFlowContext context)
			where TFlowContext : IFlowContextChangeable
		{
			return _stageServices
				.Where(s => s.FlowStageIdentifier == context.CurrentStage)
				.OfType<IFlowStage<TFlowContext>>()
				.Single();
		}
	}
}
