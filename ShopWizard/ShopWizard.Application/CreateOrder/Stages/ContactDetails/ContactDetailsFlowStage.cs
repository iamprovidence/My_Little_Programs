using FlowStage.Exceptions;
using FlowStage.Interfaces;
using FlowStage.Models;
using ShopWizard.Application.CreateOrder.Stages.ContactDetails.Commands;
using ShopWizard.Application.CreateOrder.Stages.PaymentDetails;
using ShopWizard.Application.CreateOrder.Stages.ProductSelection;
using System.Threading.Tasks;

namespace ShopWizard.Application.CreateOrder.Stages.ContactDetails
{
	public class ContactDetailsFlowStage : ICreateOrderFlowStage
	{
		public FlowStageIdentifier FlowStageIdentifier => FlowStageIdentifier.From<ContactDetailsFlowStage>();

		public Task<FlowStageIdentifier> ProcessCommand(CreateOrderFlowContext context, IFlowCommand command)
		{
			return command switch
			{
				SubmitContactDetailsCommand submitCommand => DoProcessing(context, submitCommand),
				GoToProductSelectionCommand selectProductCommand => DoProcessing(context, selectProductCommand),

				_ => throw new UnsupportedCommandException(),
			};
		}

		private async Task<FlowStageIdentifier> DoProcessing(CreateOrderFlowContext context, SubmitContactDetailsCommand command)
		{
			// Validate command
			if (command == null)
			{
				return FlowStageIdentifier.From<ContactDetailsFlowStage>();
			}

			if (!IsCommandValid(command))
			{
				context.ErrorMessage = "Enter contact data";

				return FlowStageIdentifier.From<ContactDetailsFlowStage>();
			}

			// Commit changes
			context.Email = command.Email;

			// Switch to next stage
			return FlowStageIdentifier.From<PaymentDetailsFlowStage>();
		}

		private Task<FlowStageIdentifier> DoProcessing(CreateOrderFlowContext context, GoToProductSelectionCommand command)
		{
			// Switch to previous stage
			var nextStage = FlowStageIdentifier.From<ProductSelectionFlowStage>();

			return Task.FromResult(nextStage);
		}

		private bool IsCommandValid(SubmitContactDetailsCommand command)
		{
			return !string.IsNullOrEmpty(command.Email) && command.Email.Contains("@");
		}

	}
}
