using FlowStage.Interfaces;

namespace ShopWizard.Application.CreateOrder.Stages.ContactDetails.Commands
{
	public class SubmitContactDetailsCommand : IFlowCommand
	{
		public string Email { get; set; }
	}
}
