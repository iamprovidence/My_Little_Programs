using FlowStage;
using ShopWizard.Application.CreateOrder.Interfaces;
using System.Collections.Generic;

namespace ShopWizard.Application.CreateOrder
{
	public class CreateOrderFlowService : FlowService<ICreateOrderFlowStage, CreateOrderFlowContext>
	{
		public CreateOrderFlowService(IEnumerable<ICreateOrderFlowStage> stageServices)
			: base(stageServices) { }
	}
}
