using FlowStage.Abstractions.Interfaces;
using ShopWizard.Application.CancelOrder.ViewModels;

namespace ShopWizard.Application.CancelOrder.Interfaces
{
	public interface ICancelOrderOutputPort : IOutputPort
	{
		void Home();
		void EnterOrderCode(EnterOrderCodeViewModel viewModel);
		void ConfirmCancel(ConfirmCancelViewModel viewModel);
		void Summary(SummaryViewModel viewModel);
	}
}
