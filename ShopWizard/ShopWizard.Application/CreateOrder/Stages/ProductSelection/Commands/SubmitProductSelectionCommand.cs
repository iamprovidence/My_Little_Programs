using FlowStage.Interfaces;
using ShopWizard.Domain.Enums;

namespace ShopWizard.Application.CreateOrder.Stages.ProductSelection.Commands
{
	public class SubmitProductSelectionCommand : IFlowCommand
	{
		public ProductType? Product { get; set; }
	}
}
