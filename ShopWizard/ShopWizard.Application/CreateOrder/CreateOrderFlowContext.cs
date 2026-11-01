using FlowStage.Models;
using ShopWizard.Domain.Enums;
using System.Runtime.Serialization;

namespace ShopWizard.Application.CreateOrder
{
	[DataContract]
	public class CreateOrderFlowContext : FlowContextBase
	{
		[DataMember]
		public ProductType? Product { get; set; }

		[DataMember]
		public string Email { get; set; }

		[DataMember]
		public PaymentMethod? PaymentMethod { get; set; }
	}
}
