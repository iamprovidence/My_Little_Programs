namespace ShopWizard.Application.CancelOrder.Stages.ConfirmCancel.ViewModels
{
	public class ConfirmCancelViewModel
	{
		public CancelOrderFlowContext FlowContext { get; set; }
		public bool? ShouldCancelOrder { get; set; }
	}
}
