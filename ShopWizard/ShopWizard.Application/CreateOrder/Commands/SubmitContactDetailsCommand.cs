using FlowStage.Abstractions.Interfaces;

namespace ShopWizard.Application.CreateOrder.Commands
{
	public class SubmitContactDetailsCommand : IFlowCommand
	{
		public string Email { get; set; }
	}
}
