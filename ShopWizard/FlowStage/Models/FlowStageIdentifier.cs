using FlowStage.Interfaces;
using System;
using System.Runtime.Serialization;

namespace FlowStage.Models
{
	[DataContract]
	public class FlowStageIdentifier : IEquatable<FlowStageIdentifier>
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

		public static bool operator ==(FlowStageIdentifier i1, FlowStageIdentifier i2)
		{
			return i1.Equals(i2);
		}

		public static bool operator !=(FlowStageIdentifier i1, FlowStageIdentifier i2)
		{
			return !i1.Equals(i2);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as FlowStageIdentifier);
		}

		public bool Equals(FlowStageIdentifier other)
		{
			return _identifier == other._identifier;
		}

		public override string ToString()
		{
			return _identifier;
		}

		public static implicit operator string(FlowStageIdentifier flowStageIdentifier)
		{
			return flowStageIdentifier.ToString();
		}
	}
}
