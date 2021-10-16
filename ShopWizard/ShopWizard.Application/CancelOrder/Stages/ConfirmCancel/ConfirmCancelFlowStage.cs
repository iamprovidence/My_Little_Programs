using FlowStage.Exceptions;
using FlowStage.Interfaces;
using FlowStage.Models;
using ShopWizard.Application.CancelOrder.Stages.CancelOrderComplete;
using ShopWizard.Application.CancelOrder.Stages.ConfirmCancel.Commands;
using System;
using System.Threading.Tasks;

namespace ShopWizard.Application.CancelOrder.Stages.ConfirmCancel
{
	public class ConfirmCancelFlowStage : ICancelOrderFlowStage
	{
		public FlowStageIdentifier FlowStageIdentifier => FlowStageIdentifier.From<ConfirmCancelFlowStage>();

		public Task<FlowStageIdentifier> ProcessCommand(CancelOrderFlowContext context, IFlowCommand command)
		{
			return command switch
			{
				SubmitCancelOrderCommand submitCommand => DoProcessing(context, submitCommand),

				_ => throw new UnsupportedCommandException(),
			};
		}

		private async Task<FlowStageIdentifier> DoProcessing(CancelOrderFlowContext context, SubmitCancelOrderCommand command)
		{
			if (!command.ShouldCancelOrder.HasValue)
			{
				context.ErrorMessage = "Chose option";

				return FlowStageIdentifier.From<ConfirmCancelFlowStage>();
			}

			if (command.ShouldCancelOrder == false)
			{
				return FlowStageIdentifier.From<CancelOrderCompleteFlowStage>();
			}

			// Perform action
			context.DidSucceed = CancelOrder();

			// Switch to next stage
			return FlowStageIdentifier.From<CancelOrderCompleteFlowStage>();
		}

		private bool CancelOrder()
		{
			return new Random().NextDouble() > 0.5;
		}
	}
}
