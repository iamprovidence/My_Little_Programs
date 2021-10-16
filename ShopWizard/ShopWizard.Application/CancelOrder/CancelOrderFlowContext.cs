using FlowStage.Interfaces;
using FlowStage.Models;
using System.Runtime.Serialization;

namespace ShopWizard.Application.CancelOrder
{
	[DataContract]
	public class CancelOrderFlowContext : IFlowContext, IFlowContextChangeable
	{
		[DataMember]
		public FlowStageIdentifier CurrentStage { get; private set; }

		[DataMember]
		public string ErrorMessage { get; set; }

		[DataMember]
		public string OrderCode { get; set; }

		[DataMember]
		public bool DidSucceed { get; set; }

		void IFlowContextChangeable.Reset()
		{
			ErrorMessage = null;
		}

		void IFlowContextChangeable.SetCurrentStage(FlowStageIdentifier newStage)
		{
			CurrentStage = newStage;
		}
	}
}
