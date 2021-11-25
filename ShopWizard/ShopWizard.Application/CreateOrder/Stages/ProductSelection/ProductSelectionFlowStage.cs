using FlowStage.Exceptions;
using FlowStage.Interfaces;
using FlowStage.Models;
using ShopWizard.Application.CreateOrder.Stages.ContactDetails;
using ShopWizard.Application.CreateOrder.Stages.ProductSelection.Commands;
using System.Threading.Tasks;

namespace ShopWizard.Application.CreateOrder.Stages.ProductSelection
{
	public class ProductSelectionFlowStage : ICreateOrderFlowStage
	{
		public FlowStageIdentifier FlowStageIdentifier => FlowStageIdentifier.From<ProductSelectionFlowStage>();

		public Task<FlowStageIdentifier> ProcessCommand(CreateOrderFlowContext context, IFlowCommand command)
		{
			return command switch
			{
				SubmitProductSelectionCommand submitCommand => DoProcessing(context, submitCommand),

				_ => throw new UnsupportedCommandException(),
			};
		}

		private async Task<FlowStageIdentifier> DoProcessing(CreateOrderFlowContext context, SubmitProductSelectionCommand command)
		{
			// Validate command
			if (command == null)
			{
				return FlowStageIdentifier.From<ProductSelectionFlowStage>();
			}

			if (!IsCommandValid(command))
			{
				context.ErrorMessage = "Select product";

				return FlowStageIdentifier.From<ProductSelectionFlowStage>();
			}

			// Logic here
			
			// Commit changes
			context.Product = command.Product;

			// Switch to next stage
			return FlowStageIdentifier.From<ContactDetailsFlowStage>();
		}

		private bool IsCommandValid(SubmitProductSelectionCommand command)
		{
			return command.Product.HasValue;
		}
	}
}
