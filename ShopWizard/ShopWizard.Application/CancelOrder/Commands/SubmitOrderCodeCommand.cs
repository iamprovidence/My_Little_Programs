using FlowStage.Abstractions.Interfaces;

namespace ShopWizard.Application.CancelOrder.Commands
{
	public class SubmitOrderCodeCommand : IFlowCommand
	{
		public string OrderCode { get; set; }
	}
}
