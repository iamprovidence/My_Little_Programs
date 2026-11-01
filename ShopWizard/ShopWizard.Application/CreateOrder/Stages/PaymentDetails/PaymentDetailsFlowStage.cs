using FlowStage.Commands;
using FlowStage.Exceptions;
using FlowStage.Interfaces;
using FlowStage.Models;
using ShopWizard.Application.CreateOrder.Stages.PaymentDetails.Commands;
using ShopWizard.Application.CreateOrder.Stages.Summary;
using System.Threading.Tasks;

namespace ShopWizard.Application.CreateOrder.Stages.PaymentDetails
{
	public class PaymentDetailsFlowStage : ICreateOrderFlowStage
	{
		public FlowStageIdentifier FlowStageIdentifier => FlowStageIdentifier.From<PaymentDetailsFlowStage>();

		public Task<FlowStageIdentifier> ProcessCommand(CreateOrderFlowContext context, IFlowCommand command)
		{
			return command switch
			{
				SubmitPaymentDetailsCommand submitCommand => DoProcessing(context, submitCommand),
				GoBackCommand goBackCommand => DoProcessing(context, goBackCommand),

				_ => throw new UnsupportedCommandException(),
			};
		}

		private async Task<FlowStageIdentifier> DoProcessing(CreateOrderFlowContext context, SubmitPaymentDetailsCommand command)
		{
			// Validate command
			if (command == null)
			{
				return FlowStageIdentifier.From<PaymentDetailsFlowStage>();
			}

			if (!IsCommandValid(command))
			{
				context.ErrorMessage = "Select payment method";

				return FlowStageIdentifier.From<PaymentDetailsFlowStage>();
			}

			// Commit changes
			context.PaymentMethod = command.PaymentMethod;

			// Switch to next stage
			return FlowStageIdentifier.From<SummaryFlowStage>();
		}

		private Task<FlowStageIdentifier> DoProcessing(CreateOrderFlowContext context, GoBackCommand command)
		{
			// Switch to previous stage
			if (!context.CanGoBack)
			{
				throw new UnsupportedCommandException();
			}

			return Task.FromResult(context.PreviousStage);
		}

		private bool IsCommandValid(SubmitPaymentDetailsCommand command)
		{
			return command != null && command.PaymentMethod.HasValue;
		}
	}
}
