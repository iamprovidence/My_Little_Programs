using FlowStage.Models;
using ShopWizard.Application.CancelOrder.Stages.EnterOrderCode.ViewModels;
using System.Threading.Tasks;

namespace ShopWizard.Application.CancelOrder.Stages.EnterOrderCode
{
	public class EnterOrderCodePresenter : ICancelOrderPresenter<EnterOrderCodeViewModel>
	{
		public FlowStageIdentifier FlowStageIdentifier => FlowStageIdentifier.From<EnterOrderCodeFlowStage>();

		public Task<EnterOrderCodeViewModel> ShowView(CancelOrderFlowContext context)
		{
			var viewModel = new EnterOrderCodeViewModel
			{
				FlowContext = context,
				OrderCode = context.OrderCode,
			};

			return Task.FromResult(viewModel);
		}
	}
}
