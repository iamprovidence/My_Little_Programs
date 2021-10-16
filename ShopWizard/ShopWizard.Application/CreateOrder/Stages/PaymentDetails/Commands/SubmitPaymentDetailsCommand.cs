using FlowStage.Interfaces;
using ShopWizard.Domain.Enums;

namespace ShopWizard.Application.CreateOrder.Stages.PaymentDetails.Commands
{
	public class SubmitPaymentDetailsCommand : IFlowCommand
	{
		public PaymentMethod? PaymentMethod { get; set; }
	}
}
