using ShopWizard.Domain.Enums;

namespace ShopWizard.Application.CreateOrder.ViewModels
{
	public class ProductSelectionViewModel
	{
		public CreateOrderFlowContext FlowContext { get; set; }
		public ProductType? Product { get; set; }

	}
}
