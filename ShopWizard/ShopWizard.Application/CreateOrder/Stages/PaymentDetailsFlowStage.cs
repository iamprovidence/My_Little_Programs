using FlowStage.Abstractions.Exceptions;
using FlowStage.Abstractions.Interfaces;
using ShopWizard.Application.CreateOrder.Commands;
using ShopWizard.Application.CreateOrder.Enums;
using ShopWizard.Application.CreateOrder.Interfaces;
using System;
using System.Threading.Tasks;

namespace ShopWizard.Application.CreateOrder.Stages
{
	public class PaymentDetailsFlowStage : ICreateOrderFlowStage
	{
		private readonly ICreateOrderAppService _appService;
		private readonly IOutputPortFactory _outputPortFactory;
		private ICreateOrderOutputPort _outputPort => _outputPortFactory.MakeInstance<ICreateOrderOutputPort>();

		public string FlowStageName => CreateOrderFlowStageType.PaymentDetails.ToString();

		public PaymentDetailsFlowStage(ICreateOrderAppService appService, IOutputPortFactory outputPortFactory)
		{
			_appService = appService;
			_outputPortFactory = outputPortFactory;
		}

		public Task<string> ProcessCommand(CreateOrderFlowContext context, IFlowCommand command)
		{
			return command switch
			{
				SubmitPaymentDetailsCommand submitCommand => DoProcessing(context, submitCommand),
				GoToContactDetailsCommand goToContactCommand => DoProcessing(context, goToContactCommand),

				_ => throw new UnsupportedCommandException(),
			};
		}

		private async Task<string> DoProcessing(CreateOrderFlowContext context, SubmitPaymentDetailsCommand command)
		{
			Console.WriteLine("Stage3 processing");

			// Validation command
			if (command == null)
			{
				var viewModel = await _appService.GetPaymentPage(context, command);
				_outputPort.Payment(viewModel);

				return CreateOrderFlowStageType.PaymentDetails.ToString();
			}

			if (!IsCommandValid(command))
			{
				context.ErrorMessage = "Select payment method";

				var viewModel = await _appService.GetPaymentPage(context, command);
				_outputPort.Payment(viewModel);

				return CreateOrderFlowStageType.PaymentDetails.ToString();
			}

			// Commit changes
			context.PaymentMethod = command.PaymentMethod;

			// Show new view
			var summaryViewModel = await _appService.GetSummaryPage(context);
			_outputPort.Summary(summaryViewModel);

			// Switch to Stage4
			return CreateOrderFlowStageType.SummaryPage.ToString();
		}

		private async Task<string> DoProcessing(CreateOrderFlowContext context, GoToContactDetailsCommand command)
		{
			// Show the view
			var viewModel = await _appService.GetContactDetailsPage(context, command);
			_outputPort.ContactDetails(viewModel);

			// Switch to Stage2
			return CreateOrderFlowStageType.ContactDetails.ToString();
		}


		private bool IsCommandValid(SubmitPaymentDetailsCommand command)
		{
			return command != null && command.PaymentMethod.HasValue;
		}

	}
}
