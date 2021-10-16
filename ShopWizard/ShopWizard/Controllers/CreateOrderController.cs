using FlowStage.Interfaces;
using FlowStage.Models;
using Microsoft.AspNetCore.Mvc;
using ShopWizard.Application.CreateOrder;
using ShopWizard.Application.CreateOrder.Stages.ContactDetails;
using ShopWizard.Application.CreateOrder.Stages.ContactDetails.Commands;
using ShopWizard.Application.CreateOrder.Stages.ContactDetails.ViewModels;
using ShopWizard.Application.CreateOrder.Stages.PaymentDetails;
using ShopWizard.Application.CreateOrder.Stages.PaymentDetails.Commands;
using ShopWizard.Application.CreateOrder.Stages.PaymentDetails.ViewModels;
using ShopWizard.Application.CreateOrder.Stages.ProductSelection;
using ShopWizard.Application.CreateOrder.Stages.ProductSelection.Commands;
using ShopWizard.Application.CreateOrder.Stages.ProductSelection.ViewModels;
using ShopWizard.Application.CreateOrder.Stages.Summary;
using ShopWizard.Application.CreateOrder.Stages.Summary.ViewModels;
using ShopWizard.Controllers.Abstract;
using System.Threading.Tasks;

namespace ShopWizard.Controllers
{
	public class CreateOrderController : FlowControllerBase<CreateOrderFlowContext>
	{
		protected override FlowStageIdentifier InitialStage => FlowStageIdentifier.From<ProductSelectionFlowStage>();

		public CreateOrderController(IFlowStageOrchestrator flowOrchestrator, IFlowStagePresenterOrchestrator presenterOrchestrator)
			: base(flowOrchestrator, presenterOrchestrator) { }

		#region Product selection
		[HttpGet]
		[Route(nameof(ProductSelectionFlowStage))]
		public Task<IActionResult> ShowProductSelection()
		{
			return ShowView<ProductSelectionViewModel>();
		}

		[HttpPost]
		[Route(nameof(SubmitProductSelectionCommand))]
		public Task<IActionResult> SubmitProductSelection(SubmitProductSelectionCommand command)
		{
			return ProcessCommand(command);
		}
		#endregion

		#region Contact details
		[HttpGet]
		[Route(nameof(ContactDetailsFlowStage))]
		public Task<IActionResult> ShowContactDetails()
		{
			return ShowView<ContactDetailsViewModel>();
		}

		[HttpPost]
		[Route(nameof(GoToProductSelectionCommand))]
		public Task<IActionResult> GoToProductSelection()
		{
			return ProcessCommand(new GoToProductSelectionCommand());
		}

		[HttpPost]
		[Route(nameof(SubmitContactDetailsCommand))]
		public Task<IActionResult> SubmitContactDetails(SubmitContactDetailsCommand command)
		{
			return ProcessCommand(command);
		}
		#endregion

		#region Payment details
		[HttpGet]
		[Route(nameof(PaymentDetailsFlowStage))]
		public Task<IActionResult> ShowPaymentDetails()
		{
			return ShowView<PaymentDetailsViewModel>();
		}

		[HttpPost]
		[Route(nameof(GoToContactDetailsCommand))]
		public Task<IActionResult> GoToContactDetails()
		{
			return ProcessCommand(new GoToContactDetailsCommand());
		}

		[HttpPost]
		[Route(nameof(SubmitPaymentDetailsCommand))]
		public Task<IActionResult> SubmitPaymentDetails(SubmitPaymentDetailsCommand command)
		{
			return ProcessCommand(command);
		}
		#endregion

		#region Summary
		[HttpGet]
		[Route(nameof(SummaryFlowStage))]
		public Task<IActionResult> ShowSummary()
		{
			return ShowView<SummaryViewModel>();
		}
		#endregion
	}
}
