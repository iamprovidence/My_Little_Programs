using FlowStage.Interfaces;
using FlowStage.Models;
using ShopWizard.Domain.Enums;
using System.Runtime.Serialization;

namespace ShopWizard.Application.CreateOrder
{
	[DataContract]
	public class CreateOrderFlowContext : IFlowContext, IFlowContextChangeable
	{
		[DataMember]
		public FlowStageIdentifier CurrentStage { get; private set; }

		[DataMember]
		public string ErrorMessage { get; set; }

		[DataMember]
		public ProductType? Product { get; set; }

		[DataMember]
		public string Email { get; set; }

		[DataMember]
		public PaymentMethod? PaymentMethod { get; set; }

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
