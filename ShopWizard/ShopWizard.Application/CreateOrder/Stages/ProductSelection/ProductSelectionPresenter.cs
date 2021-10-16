using FlowStage.Models;
using ShopWizard.Application.CreateOrder.Stages.ProductSelection.ViewModels;
using System.Threading.Tasks;

namespace ShopWizard.Application.CreateOrder.Stages.ProductSelection
{
	public class ProductSelectionPresenter : ICreateOrderPresenter<ProductSelectionViewModel>
	{
		public FlowStageIdentifier FlowStageIdentifier => FlowStageIdentifier.From<ProductSelectionFlowStage>();

		public Task<ProductSelectionViewModel> ShowView(CreateOrderFlowContext context)
		{
			var viewModel = new ProductSelectionViewModel
			{
				FlowContext = context,
				Product = context.Product,
			};

			return Task.FromResult(viewModel);
		}
	}
}
