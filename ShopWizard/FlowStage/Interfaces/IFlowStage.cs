using FlowStage.Models;
using System.Threading.Tasks;

namespace FlowStage.Interfaces
{
	public interface IFlowStage
	{
		FlowStageIdentifier FlowStageIdentifier { get; }
	}

	public interface IFlowStage<TContext> : IFlowStage
		where TContext : IFlowContext
	{
		Task<FlowStageIdentifier> ProcessCommand(TContext context, IFlowCommand command);
	}
}
