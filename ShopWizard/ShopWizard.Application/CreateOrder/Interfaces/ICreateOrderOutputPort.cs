using FlowStage.Abstractions.Interfaces;
using ShopWizard.Application.CreateOrder.ViewModels;

namespace ShopWizard.Application.CreateOrder.Interfaces
{
	public interface ICreateOrderOutputPort : IOutputPort
	{
		void ContactDetails(ContactDetailsViewModel viewModel);
		void Payment(PaymentDetailsViewModel viewModel);
		void ProductSelection(ProductSelectionViewModel viewModel);
		void Summary(SummaryPageViewModel viewModel);

	}
}
