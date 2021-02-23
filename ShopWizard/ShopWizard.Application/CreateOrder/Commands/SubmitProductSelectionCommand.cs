using FlowStage.Abstractions.Interfaces;
using ShopWizard.Domain.Enums;

namespace ShopWizard.Application.CreateOrder.Commands
{
	public class SubmitProductSelectionCommand : IFlowCommand
	{
		public ProductType? Product { get; set; }
	}
}
