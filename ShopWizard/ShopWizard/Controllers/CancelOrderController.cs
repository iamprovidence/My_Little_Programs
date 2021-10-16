using FlowStage.Interfaces;
using FlowStage.Models;
using Microsoft.AspNetCore.Mvc;
using ShopWizard.Application.CancelOrder;
using ShopWizard.Application.CancelOrder.Stages.CancelOrderComplete;
using ShopWizard.Application.CancelOrder.Stages.CancelOrderComplete.ViewModels;
using ShopWizard.Application.CancelOrder.Stages.ConfirmCancel;
using ShopWizard.Application.CancelOrder.Stages.ConfirmCancel.Commands;
using ShopWizard.Application.CancelOrder.Stages.ConfirmCancel.ViewModels;
using ShopWizard.Application.CancelOrder.Stages.EnterOrderCode;
using ShopWizard.Application.CancelOrder.Stages.EnterOrderCode.Commands;
using ShopWizard.Application.CancelOrder.Stages.EnterOrderCode.ViewModels;
using ShopWizard.Controllers.Abstract;
using System.Threading.Tasks;

namespace ShopWizard.Controllers
{
	public class CancelOrderController : FlowControllerBase<CancelOrderFlowContext>
	{
		protected override FlowStageIdentifier InitialStage => FlowStageIdentifier.From<EnterOrderCodeFlowStage>();

		public CancelOrderController(IFlowStageOrchestrator flowOrchestrator, IFlowStagePresenterOrchestrator presenterOrchestrator)
			: base(flowOrchestrator, presenterOrchestrator) { }

		#region Enter order code
		[HttpGet]
		[Route(nameof(EnterOrderCodeFlowStage))]
		public Task<IActionResult> ShowEnterOrderCode()
		{
			return ShowView<EnterOrderCodeViewModel>();
		}

		[HttpPost]
		[Route(nameof(SubmitOrderCodeCommand))]
		public Task<IActionResult> SubmitOrderCode(SubmitOrderCodeCommand command)
		{
			return ProcessCommand(command);
		}
		#endregion

		#region Confirm cancel
		[HttpGet]
		[Route(nameof(ConfirmCancelFlowStage))]
		public Task<IActionResult> ShowConfirmCancel()
		{
			return ShowView<ConfirmCancelViewModel>();
		}

		[HttpPost]
		[Route(nameof(SubmitCancelOrderCommand))]
		public Task<IActionResult> SubmitOrderCancelation(SubmitCancelOrderCommand command)
		{
			return ProcessCommand(command);
		}
		#endregion

		#region Summary
		[HttpGet]
		[Route(nameof(CancelOrderCompleteFlowStage))]
		public Task<IActionResult> ShowSummary()
		{
			return ShowView<CancelOrderCompleteViewModel>();
		}
		#endregion
	}
}
