using ShopWizard.Application.CancelOrder.Commands;
using ShopWizard.Application.CancelOrder.ViewModels;
using System.Threading.Tasks;

namespace ShopWizard.Application.CancelOrder.Interfaces
{
	public interface ICancelOrderAppService
	{
		Task<bool> DoesOrderExist(string orderCode);
		Task<SummaryViewModel> CancelOrder(CancelOrderFlowContext context);
		Task<EnterOrderCodeViewModel> GetEnterOrderCodePage(CancelOrderFlowContext context);
		Task<EnterOrderCodeViewModel> GetEnterOrderCodePage(CancelOrderFlowContext context, SubmitOrderCodeCommand command);
		Task<ConfirmCancelViewModel> GetConfirmationPage(CancelOrderFlowContext context);
	}
}
