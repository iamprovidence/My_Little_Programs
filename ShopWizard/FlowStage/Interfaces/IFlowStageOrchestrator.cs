using System.Threading.Tasks;

namespace FlowStage.Interfaces
{
	public interface IFlowStageOrchestrator
	{
		Task ProcessCommand<TFlowContext>(TFlowContext context, IFlowCommand command)
			where TFlowContext : IFlowContextChangeable;
	}
}
