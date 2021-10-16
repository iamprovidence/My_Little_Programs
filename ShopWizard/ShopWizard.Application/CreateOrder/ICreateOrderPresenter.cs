using FlowStage.Interfaces;

namespace ShopWizard.Application.CreateOrder
{
	internal interface ICreateOrderPresenter<TViewModel> : IFlowStagePresenter<CreateOrderFlowContext, TViewModel>
	{
	}
}
