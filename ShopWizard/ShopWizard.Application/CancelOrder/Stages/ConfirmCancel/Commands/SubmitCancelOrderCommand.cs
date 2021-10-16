using FlowStage.Interfaces;

namespace ShopWizard.Application.CancelOrder.Stages.ConfirmCancel.Commands
{
	public class SubmitCancelOrderCommand : IFlowCommand
	{
		public bool? ShouldCancelOrder { get; set; }
	}
}
