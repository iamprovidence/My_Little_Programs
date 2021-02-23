using FlowStage.Abstractions.Interfaces;
using ShopWizard.Domain.Enums;

namespace ShopWizard.Application.CreateOrder.Commands
{
	public class SubmitPaymentDetailsCommand : IFlowCommand
	{
		public PaymentMethod? PaymentMethod { get; set; }
	}
}
