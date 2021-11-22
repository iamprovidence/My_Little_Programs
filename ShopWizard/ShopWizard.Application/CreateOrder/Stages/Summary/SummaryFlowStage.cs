using FlowStage.Exceptions;
using FlowStage.Interfaces;
using FlowStage.Models;
using System.Threading.Tasks;

namespace ShopWizard.Application.CreateOrder.Stages.Summary
{
	public class SummaryFlowStage : ICreateOrderFlowStage
	{
		public FlowStageIdentifier FlowStageIdentifier => FlowStageIdentifier.From<SummaryFlowStage>();

		public Task<FlowStageIdentifier> ProcessCommand(CreateOrderFlowContext context, IFlowCommand command)
		{
			// Termination state
			return command switch
			{
				_ => throw new UnsupportedCommandException(),
			};
		}
	}
}
