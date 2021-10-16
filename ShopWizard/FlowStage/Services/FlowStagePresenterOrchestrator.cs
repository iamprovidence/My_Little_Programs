using FlowStage.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowStage.Services
{
	public class FlowStagePresenterOrchestrator : IFlowStagePresenterOrchestrator
	{
		private readonly IEnumerable<IFlowStagePresenter> _stagePresenters;

		public FlowStagePresenterOrchestrator(IEnumerable<IFlowStagePresenter> stagePresenters)
		{
			_stagePresenters = stagePresenters;
		}

		public Task<TViewModel> ShowView<TFlowContext, TViewModel>(TFlowContext context)
			where TFlowContext : IFlowContext
		{
			var presenter = GetStagePresenter<TFlowContext, TViewModel>(context);

			return presenter.ShowView(context);
		}

		private IFlowStagePresenter<TFlowContext, TViewModel> GetStagePresenter<TFlowContext, TViewModel>(IFlowContext context)
			where TFlowContext : IFlowContext
		{
			return _stagePresenters
				.Where(p => p.FlowStageIdentifier == context.CurrentStage)
				.OfType<IFlowStagePresenter<TFlowContext, TViewModel>>()
				.Single();
		}
	}
}
