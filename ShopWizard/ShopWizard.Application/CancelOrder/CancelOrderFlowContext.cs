using FlowStage.Models;
using System.Runtime.Serialization;

namespace ShopWizard.Application.CancelOrder
{
	[DataContract]
	public class CancelOrderFlowContext : FlowContextBase
	{
		[DataMember]
		public string OrderCode { get; set; }

		[DataMember]
		public bool DidSucceed { get; set; }
	}
}
