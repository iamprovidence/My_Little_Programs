using System.Threading.Tasks;

namespace FlowStage.Abstractions.Interfaces
{
	public interface IFlowStage<TContext>
		where TContext : IFlowContext
	{
		// TODO: class FlowStateId
		string FlowStageName { get; }

		Task<string> ProcessCommand(TContext context, IFlowCommand command);
	}
}
