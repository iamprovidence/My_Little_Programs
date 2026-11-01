using FlowStage.Interfaces;
using System.Runtime.Serialization;

namespace FlowStage.Models
{
	// Not positional, so From<TFlowStage>() stays the only way to create one
	[DataContract]
	public sealed record FlowStageIdentifier
	{
		[DataMember]
		private readonly string _identifier;

		private FlowStageIdentifier() { }

		private FlowStageIdentifier(string identifier)
		{
			_identifier = identifier;
		}

		public static FlowStageIdentifier From<TFlowStage>()
			where TFlowStage : IFlowStage
		{
			return new FlowStageIdentifier(typeof(TFlowStage).Name);
		}

		public override string ToString()
		{
			return _identifier;
		}

		public static implicit operator string(FlowStageIdentifier flowStageIdentifier)
		{
			return flowStageIdentifier?.ToString();
		}
	}
}
