using FlowStage.Abstractions.Interfaces;
using ShopWizard.Application.CreateOrder.Enums;
using ShopWizard.Domain.Enums;
using System.Runtime.Serialization;

namespace ShopWizard.Application.CreateOrder
{
	[DataContract]
	public class CreateOrderFlowContext : IFlowContext, IFlowContextChangeable
	{
		[DataMember]
		public string StageName { get; private set; }

		[DataMember]
		public string ErrorMessage { get; set; }

		[DataMember]
		public ProductType? Product { get; set; }

		[DataMember]
		public string Email { get; set; }

		[DataMember]
		public PaymentMethod? PaymentMethod { get; set; }

		public CreateOrderFlowContext()
		{
			StageName = CreateOrderFlowStageType.ProductSelection.ToString();
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
