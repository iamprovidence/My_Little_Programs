using FlowStage.Models;
using ShopWizard.Application.CancelOrder.Stages.ConfirmCancel.ViewModels;
using System.Threading.Tasks;

namespace ShopWizard.Application.CancelOrder.Stages.ConfirmCancel
{
	public class ConfirmCancelPresenter : ICancelOrderPresenter<ConfirmCancelViewModel>
	{
		public FlowStageIdentifier FlowStageIdentifier => FlowStageIdentifier.From<ConfirmCancelFlowStage>();

		public Task<ConfirmCancelViewModel> ShowView(CancelOrderFlowContext context)
		{
			var viewModel = new ConfirmCancelViewModel
			{
				FlowContext = context,
				ShouldCancelOrder = null,
			};

			return Task.FromResult(viewModel);
		}
	}
}
