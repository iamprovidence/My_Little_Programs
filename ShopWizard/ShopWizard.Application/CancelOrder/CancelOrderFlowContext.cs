using FlowStage.Abstractions.Interfaces;
using ShopWizard.Application.CancelOrder.Enums;
using System.Runtime.Serialization;

namespace ShopWizard.Application.CancelOrder
{
	[DataContract]
	public class CancelOrderFlowContext : IFlowContext, IFlowContextChangeable
	{
		[DataMember]
		public string StageName { get; private set; }

		[DataMember]
		public string ErrorMessage { get; set; }

		[DataMember]
		public string OrderCode { get; set; }

		
		public CancelOrderFlowContext()
		{
			StageName = CancelOrderFlowStageType.EnderOrderCode.ToString();
		}

		void IFlowContextChangeable.Reset()
		{
			ErrorMessage = null;
		}

		void IFlowContextChangeable.SetStage(string newStage)
		{
			StageName = newStage;
		}
	}
}
