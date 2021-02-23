using FlowStage.Abstractions.Interfaces;

namespace ShopWizard.Application.CancelOrder.Commands
{
	public class SubmitCancelOrderCommand : IFlowCommand
	{
		public bool? ShouldCancelOrder { get; set; }
	}
}
