using ShopWizard.Application.CreateOrder.Commands;
using ShopWizard.Application.CreateOrder.ViewModels;
using System.Threading.Tasks;

namespace ShopWizard.Application.CreateOrder.Interfaces
{
	public interface ICreateOrderAppService
	{
		Task<ContactDetailsViewModel> GetContactDetailsPage(CreateOrderFlowContext context, SubmitContactDetailsCommand command);
		Task<ContactDetailsViewModel> GetContactDetailsPage(CreateOrderFlowContext context, GoToContactDetailsCommand command);
		Task<ContactDetailsViewModel> GetContactDetailsPage(CreateOrderFlowContext context, SubmitProductSelectionCommand command);
		

		Task<ProductSelectionViewModel> GetProductListPage(CreateOrderFlowContext context, SubmitProductSelectionCommand command);
		Task<ProductSelectionViewModel> GetProductListPage(CreateOrderFlowContext context, GoToProductSelectionCommand command);
		

		Task<PaymentDetailsViewModel> GetPaymentPage(CreateOrderFlowContext context, SubmitPaymentDetailsCommand command);
		Task<PaymentDetailsViewModel> GetPaymentPage(CreateOrderFlowContext context, SubmitContactDetailsCommand command);


		Task<SummaryPageViewModel> GetSummaryPage(CreateOrderFlowContext context);
		Task<SummaryPageViewModel> GetSummaryPage(CreateOrderFlowContext context, SubmitPaymentDetailsCommand command);
	}
}
