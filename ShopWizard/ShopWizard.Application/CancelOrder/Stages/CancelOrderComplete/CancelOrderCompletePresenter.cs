using FlowStage.Models;
using ShopWizard.Application.CancelOrder.Stages.CancelOrderComplete.ViewModels;
using System.Threading.Tasks;

namespace ShopWizard.Application.CancelOrder.Stages.CancelOrderComplete
{
	public class CancelOrderCompletePresenter : ICancelOrderPresenter<CancelOrderCompleteViewModel>
	{
		public FlowStageIdentifier FlowStageIdentifier => FlowStageIdentifier.From<CancelOrderCompleteFlowStage>();

		public Task<CancelOrderCompleteViewModel> ShowView(CancelOrderFlowContext context)
		{
			var viewModel = new CancelOrderCompleteViewModel
			{
				FlowContext = context,
				DidSucceed = context.DidSucceed,
			};

			return Task.FromResult(viewModel);
		}
	}
}
