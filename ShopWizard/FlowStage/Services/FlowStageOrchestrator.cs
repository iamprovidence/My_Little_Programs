using FlowStage.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowStage.Services
{
	public class FlowStageOrchestrator : IFlowStageOrchestrator
	{
		private readonly IEnumerable<IFlowStart> _flowStarts;
		private readonly IEnumerable<IFlowStage> _stageServices;

		public FlowStageOrchestrator(IEnumerable<IFlowStart> flowStarts, IEnumerable<IFlowStage> stageServices)
		{
			_flowStarts = flowStarts;
			_stageServices = stageServices;
		}

		public async Task StartFlow<TFlowContext>(TFlowContext context)
			where TFlowContext : IFlowContextChangeable
		{
			var flowStart = GetFlowStart<TFlowContext>();
			var initialStage = await flowStart.Start(context);

			context.SetCurrentStage(initialStage);
		}

		public async Task ProcessCommand<TFlowContext>(TFlowContext context, IFlowCommand command)
			where TFlowContext : IFlowContextChangeable
		{
			context.Reset();

			var stageService = GetFlowStageService<TFlowContext>(context);
			var nextStageName = await stageService.ProcessCommand(context, command);

			context.SetCurrentStage(nextStageName);
		}

		private IFlowStart<TFlowContext> GetFlowStart<TFlowContext>()
			where TFlowContext : IFlowContextChangeable
		{
			return _flowStarts
				.OfType<IFlowStart<TFlowContext>>()
				.Single();
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
