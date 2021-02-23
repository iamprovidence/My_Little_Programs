using System.Threading.Tasks;

namespace FlowStage.Abstractions.Interfaces
{
	public interface IFlowService<TContext>
		where TContext : IFlowContext
	{
		Task ProcessCommand(TContext context, IFlowCommand command);
	}
}
