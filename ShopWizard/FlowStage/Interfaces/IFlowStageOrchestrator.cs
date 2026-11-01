using System.Threading.Tasks;

namespace FlowStage.Interfaces
{
	public interface IFlowStageOrchestrator
	{
		Task StartFlow<TFlowContext>(TFlowContext context)
			where TFlowContext : IFlowContextChangeable;

		Task ProcessCommand<TFlowContext>(TFlowContext context, IFlowCommand command)
			where TFlowContext : IFlowContextChangeable;
	}
}
