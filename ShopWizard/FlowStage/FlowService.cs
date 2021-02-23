using FlowStage.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowStage
{
	// FlowStages use IOutputPortFactory
	public class FlowService<TFlowStage, TFlowContext> : IFlowService<TFlowContext>
		where TFlowStage : IFlowStage<TFlowContext>
		where TFlowContext : IFlowContextChangeable
	{
		private readonly IEnumerable<TFlowStage> _stages;

		public FlowService(IEnumerable<TFlowStage> stageServices)
		{
			_stages = stageServices;
		}

		public async Task ProcessCommand(TFlowContext context, IFlowCommand command)
		{
			context.Reset();

			var stage = GetFlowState(context);
			var nextStageName = await stage.ProcessCommand(context, command);

			context.SetStage(nextStageName);
		}

		private TFlowStage GetFlowState(IFlowContext context)
		{
			return _stages.First(s => s.FlowStageName == context.StageName);
		}
	}

	// FlowStages use IOutputPort
	public class LazyLoadedFlowService<TFlowStage, TFlowContext> : IFlowService<TFlowContext>
		where TFlowStage : IFlowStage<TFlowContext>
		where TFlowContext : IFlowContextChangeable
	{
		private readonly Lazy<IEnumerable<TFlowStage>> _stages;

		public LazyLoadedFlowService(Lazy<IEnumerable<TFlowStage>> stageServices)
		{
			_stages = stageServices;
		}

		public async Task ProcessCommand(TFlowContext context, IFlowCommand command)
		{
			context.Reset();

			var stage = GetFlowState(context);
			var nextStageName = await stage.ProcessCommand(context, command);

			context.SetStage(nextStageName);
		}

		private TFlowStage GetFlowState(IFlowContext context)
		{
			return _stages.Value.First(s => s.FlowStageName == context.StageName);
		}
	}
}
