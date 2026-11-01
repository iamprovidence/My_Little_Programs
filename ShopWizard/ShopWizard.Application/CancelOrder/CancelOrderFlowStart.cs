using FlowStage.Interfaces;
using FlowStage.Models;
using ShopWizard.Application.CancelOrder.Stages.EnterOrderCode;
using System.Threading.Tasks;

namespace ShopWizard.Application.CancelOrder
{
	public class CancelOrderFlowStart : IFlowStart<CancelOrderFlowContext>
	{
		public Task<FlowStageIdentifier> Start(CancelOrderFlowContext context)
		{
			return Task.FromResult(FlowStageIdentifier.From<EnterOrderCodeFlowStage>());
		}
	}
}
