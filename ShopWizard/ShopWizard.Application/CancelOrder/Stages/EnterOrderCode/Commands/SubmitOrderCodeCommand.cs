using FlowStage.Interfaces;

namespace ShopWizard.Application.CancelOrder.Stages.EnterOrderCode.Commands
{
	public class SubmitOrderCodeCommand : IFlowCommand
	{
		public string OrderCode { get; set; }
	}
}
