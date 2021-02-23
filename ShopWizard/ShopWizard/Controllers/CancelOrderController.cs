using Microsoft.AspNetCore.Mvc;
using ShopWizard.Application.CancelOrder;
using ShopWizard.Application.CancelOrder.Commands;
using ShopWizard.Application.CancelOrder.Enums;
using ShopWizard.Application.CancelOrder.Interfaces;
using ShopWizard.Application.CancelOrder.ViewModels;
using ShopWizard.Controllers.Abstract;
using System.Threading.Tasks;

namespace ShopWizard.Controllers
{
	public class CancelOrderController : FlowControllerBase<CancelOrderFlowContext>, ICancelOrderOutputPort
	{
		public CancelOrderController(CancelOrderFlowService flowService)
			: base(flowService) { }

		public Task<IActionResult> Index()
		{
			return ProcessCommand(null, new GoToEnterOrderCodeCommand());
		}

		[HttpPost(nameof(CancelOrderCommandType.SubmitOrderCode))]
		public Task<IActionResult> SubmitOrderCode(string flowState, SubmitOrderCodeCommand command)
		{
			return ProcessCommand(flowState, command);
		}

		[HttpPost(nameof(CancelOrderCommandType.SubmitOrderCancelation))]
		public Task<IActionResult> SubmitOrderCancelation(string flowState, SubmitCancelOrderCommand command)
		{
			return ProcessCommand(flowState, command);
		}

		public void EnterOrderCode(EnterOrderCodeViewModel viewModel)
		{
			Return(View("EnterOrderCode", viewModel));
		}

		public void Home()
		{
			Return(RedirectToAction(actionName: nameof(HomeController.Index), controllerName: nameof(HomeController).Replace("Controller", string.Empty)));
		}

		public void ConfirmCancel(ConfirmCancelViewModel viewModel)
		{
			Return(View("ConfirmCancel", viewModel));
		}

		public void Summary(SummaryViewModel viewModel)
		{
			Return(View("Summary", viewModel));
		}
	}
}
