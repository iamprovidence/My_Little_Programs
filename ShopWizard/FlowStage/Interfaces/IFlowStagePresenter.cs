using FlowStage.Models;
using System.Threading.Tasks;

namespace FlowStage.Interfaces
{
	public interface IFlowStagePresenter
	{
		FlowStageIdentifier FlowStageIdentifier { get; }
	}

	public interface IFlowStagePresenter<TContext, TViewModel> : IFlowStagePresenter
		where TContext : IFlowContext
	{
		Task<TViewModel> ShowView(TContext context);
	}
}
