using FlowStage.Models;
using ShopWizard.Application.CreateOrder.Stages.PaymentDetails.ViewModels;
using System.Threading.Tasks;

namespace ShopWizard.Application.CreateOrder.Stages.PaymentDetails
{
	public class PaymentDetailsPresenter : ICreateOrderPresenter<PaymentDetailsViewModel>
	{
		public FlowStageIdentifier FlowStageIdentifier => FlowStageIdentifier.From<PaymentDetailsFlowStage>();

		public Task<PaymentDetailsViewModel> ShowView(CreateOrderFlowContext context)
		{
			var viewModel = new PaymentDetailsViewModel
			{
				FlowContext = context,
				PaymentMethod = context.PaymentMethod,
			};

			return Task.FromResult(viewModel);
		}
	}
}
