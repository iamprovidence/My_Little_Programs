using ShopWizard.Domain.Enums;

namespace ShopWizard.Application.CreateOrder.ViewModels
{
	public class PaymentDetailsViewModel
	{
		public CreateOrderFlowContext FlowContext { get; set; }
		public PaymentMethod? PaymentMethod { get; set; }
	}
}
