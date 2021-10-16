using FlowStage.Exceptions;
using FlowStage.Interfaces;
using FlowStage.Models;
using ShopWizard.Application.CancelOrder.Stages.ConfirmCancel;
using ShopWizard.Application.CancelOrder.Stages.EnterOrderCode.Commands;
using System.Threading.Tasks;

namespace ShopWizard.Application.CancelOrder.Stages.EnterOrderCode
{
	public class EnterOrderCodeFlowStage : ICancelOrderFlowStage
	{
		public FlowStageIdentifier FlowStageIdentifier => FlowStageIdentifier.From<EnterOrderCodeFlowStage>();

		public Task<FlowStageIdentifier> ProcessCommand(CancelOrderFlowContext context, IFlowCommand command)
		{
			return command switch
			{
				SubmitOrderCodeCommand submitCommand => DoProcessing(context, submitCommand),

				_ => throw new UnsupportedCommandException(),
			};
		}

		private async Task<FlowStageIdentifier> DoProcessing(CancelOrderFlowContext context, SubmitOrderCodeCommand command)
		{
			if (string.IsNullOrWhiteSpace(command?.OrderCode))
			{
				context.ErrorMessage = "Enter order code";

				return FlowStageIdentifier.From<EnterOrderCodeFlowStage>();
			}

			if (!await DoesOrderExist(command.OrderCode))
			{
				context.ErrorMessage = "Order not found. Enter another code";

				return FlowStageIdentifier.From<EnterOrderCodeFlowStage>();
			}

			// Commit changes
			context.OrderCode = command.OrderCode;

			// Switch to next stage
			return FlowStageIdentifier.From<ConfirmCancelFlowStage>();
		}

		private Task<bool> DoesOrderExist(string orderCode)
		{
			var doesOrderExit = orderCode == "1111";

			return Task.FromResult(doesOrderExit);
		}
	}
}
