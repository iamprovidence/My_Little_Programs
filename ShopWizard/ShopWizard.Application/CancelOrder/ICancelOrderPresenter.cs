using FlowStage.Interfaces;

namespace ShopWizard.Application.CancelOrder
{
	internal interface ICancelOrderPresenter<TViewModel> : IFlowStagePresenter<CancelOrderFlowContext, TViewModel>
	{
	}
}
