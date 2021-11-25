using FlowStage.Exceptions;
using FlowStage.Interfaces;
using FlowStage.Models;
using System.Threading.Tasks;

namespace ShopWizard.Application.CancelOrder.Stages.CancelOrderComplete
{
	public class CancelOrderCompleteFlowStage : ICancelOrderFlowStage
	{
		public FlowStageIdentifier FlowStageIdentifier => FlowStageIdentifier.From<CancelOrderCompleteFlowStage>();

		public Task<FlowStageIdentifier> ProcessCommand(CancelOrderFlowContext context, IFlowCommand command)
		{
			// Termination state
			return command switch
			{
				_ => throw new UnsupportedCommandException(),
			};
		}
	}
}
