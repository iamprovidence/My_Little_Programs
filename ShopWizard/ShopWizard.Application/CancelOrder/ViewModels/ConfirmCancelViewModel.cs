namespace ShopWizard.Application.CancelOrder.ViewModels
{
	public class ConfirmCancelViewModel
	{
		public CancelOrderFlowContext FlowContext { get; set; }
		public bool? ShouldCancelOrder { get; set; }
	}
}
