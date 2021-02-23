using FlowStage.Abstractions.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ShopWizard.Application.CreateOrder;
using ShopWizard.Application.CreateOrder.Commands;
using ShopWizard.Application.CreateOrder.Enums;
using ShopWizard.Application.CreateOrder.Interfaces;
using ShopWizard.Application.CreateOrder.ViewModels;
using ShopWizard.Controllers.Abstract;
using System.Threading.Tasks;

namespace ShopWizard.Controllers
{
	// TODO: redirect to get for back button in browser
	public class CreateOrderController : FlowControllerBase<CreateOrderFlowContext>, ICreateOrderOutputPort
	{
		public CreateOrderController(CreateOrderFlowService flowService, IOutputPortFactory outputPortFactory)
			: base(flowService)
		{
			outputPortFactory.Register<ICreateOrderOutputPort>(this);
		}

		public Task<IActionResult> Index()
		{
			return ProcessCommand(null, new GoToProductSelectionCommand());
		}

		[HttpPost(nameof(CreateOrderCommandType.SubmitProductSelection))]
		public Task<IActionResult> SubmitProductSelection(string flowState, SubmitProductSelectionCommand command)
		{
			return ProcessCommand(flowState, command);
		}

		[HttpPost(nameof(CreateOrderCommandType.SubmitContactDetails))]
		public Task<IActionResult> SubmitContactDetails(string flowState, SubmitContactDetailsCommand command)
		{
			return ProcessCommand(flowState, command);
		}

		[HttpPost(nameof(CreateOrderCommandType.SubmitPaymentDetails))]
		public Task<IActionResult> SubmitPaymentDetails(string flowState, SubmitPaymentDetailsCommand command)
		{
			return ProcessCommand(flowState, command);
		}

		[HttpPost(nameof(CreateOrderCommandType.GoToProductSelection))]
		public Task<IActionResult> GoToProductSelection(string flowState)
		{
			return ProcessCommand(flowState, new GoToProductSelectionCommand());
		}

		[HttpPost(nameof(CreateOrderCommandType.GoToContactDetails))]
		public Task<IActionResult> GoToContactDetails(string flowState)
		{
			return ProcessCommand(flowState, new GoToContactDetailsCommand());
		}

		public void ContactDetails(ContactDetailsViewModel viewModel)
		{
			Return(View("ContactDetails", viewModel));
		}

		public void Payment(PaymentDetailsViewModel viewModel)
		{
			Return(View("PaymentDetails", viewModel));
		}

		public void ProductSelection(ProductSelectionViewModel viewModel)
		{
			Return(View("ProductSelection", viewModel));
		}

		public void Summary(SummaryPageViewModel viewModel)
		{
			Return(View("Summary", viewModel));
		}
	}
}
