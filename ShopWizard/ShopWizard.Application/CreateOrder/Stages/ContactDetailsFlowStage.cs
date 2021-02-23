using FlowStage.Abstractions.Exceptions;
using FlowStage.Abstractions.Interfaces;
using ShopWizard.Application.CreateOrder.Commands;
using ShopWizard.Application.CreateOrder.Enums;
using ShopWizard.Application.CreateOrder.Interfaces;
using System;
using System.Threading.Tasks;

namespace ShopWizard.Application.CreateOrder.Stages
{
	public class ContactDetailsFlowStage : ICreateOrderFlowStage
	{
		private readonly ICreateOrderAppService _appService;
		private readonly IOutputPortFactory _outputPortFactory;
		private ICreateOrderOutputPort _outputPort => _outputPortFactory.MakeInstance<ICreateOrderOutputPort>();

		public string FlowStageName => CreateOrderFlowStageType.ContactDetails.ToString();

		public ContactDetailsFlowStage(ICreateOrderAppService appService, IOutputPortFactory outputPortFactory)
		{
			_appService = appService;
			_outputPortFactory = outputPortFactory;
		}

		public Task<string> ProcessCommand(CreateOrderFlowContext context, IFlowCommand command)
		{
			return command switch
			{
				SubmitContactDetailsCommand submitCommand => DoProcessing(context, submitCommand),
				GoToProductSelectionCommand selectProductCommand => DoProcessing(context, selectProductCommand),

				_ => throw new UnsupportedCommandException(),
			};
		}

		private async Task<string> DoProcessing(CreateOrderFlowContext context, SubmitContactDetailsCommand command)
		{
			Console.WriteLine("Stage2 processing");

			// Validation command
			if (command == null)
			{
				var viewModel = await _appService.GetContactDetailsPage(context, command);
				_outputPort.ContactDetails(viewModel);

				return CreateOrderFlowStageType.ContactDetails.ToString();
			}

			if (!IsCommandValid(command))
			{
				context.ErrorMessage = "Enter contact data";

				var viewModel = await _appService.GetContactDetailsPage(context, command);
				_outputPort.ContactDetails(viewModel);

				return CreateOrderFlowStageType.ContactDetails.ToString();
			}

			// Commit changes
			context.Email = command.Email;

			// Show the view
			var paymentViewModel = await _appService.GetPaymentPage(context, command);
			_outputPort.Payment(paymentViewModel);

			// Switch to Stage3
			return CreateOrderFlowStageType.PaymentDetails.ToString();
		}

		private async Task<string> DoProcessing(CreateOrderFlowContext context, GoToProductSelectionCommand command)
		{
			var viewModel = await _appService.GetProductListPage(context, command);
			_outputPort.ProductSelection(viewModel);

			return CreateOrderFlowStageType.ProductSelection.ToString();
		}

		private bool IsCommandValid(SubmitContactDetailsCommand command)
		{
			return !string.IsNullOrEmpty(command.Email) && command.Email.Contains("@");
		}

	}
}
