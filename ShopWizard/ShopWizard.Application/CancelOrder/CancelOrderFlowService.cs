using FlowStage;
using ShopWizard.Application.CancelOrder.Interfaces;
using System;
using System.Collections.Generic;

namespace ShopWizard.Application.CancelOrder
{
	public class CancelOrderFlowService : LazyLoadedFlowService<ICancelOrderFlowStage, CancelOrderFlowContext>
	{
		public CancelOrderFlowService(Lazy<IEnumerable<ICancelOrderFlowStage>> stageServices)
			: base(stageServices) { }
	}
}
