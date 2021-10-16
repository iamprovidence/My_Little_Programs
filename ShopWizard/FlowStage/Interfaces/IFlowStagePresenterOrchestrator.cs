using System.Threading.Tasks;

namespace FlowStage.Interfaces
{
	public interface IFlowStagePresenterOrchestrator
	{
		Task<TViewModel> ShowView<TFlowContext, TViewModel>(TFlowContext context)
			where TFlowContext : IFlowContext;
	}
}
