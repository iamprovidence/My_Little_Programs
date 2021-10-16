using ShopWizard.Domain.Enums;

namespace ShopWizard.Application.CreateOrder.Stages.PaymentDetails.ViewModels
{
	public class PaymentDetailsViewModel
	{
		public CreateOrderFlowContext FlowContext { get; set; }
		public PaymentMethod? PaymentMethod { get; set; }
	}
}
