using FlowStage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace FlowStage.Models
{
	[DataContract]
	public abstract class FlowContextBase : IFlowContextChangeable
	{
		[DataMember]
		public FlowStageIdentifier CurrentStage { get; private set; }

		[DataMember]
		public string ErrorMessage { get; set; }

		// List instead of Stack, because JSON deserialization reverses a Stack
		[DataMember]
		private List<FlowStageIdentifier> _visitedStages = new();

		public FlowStageIdentifier PreviousStage => _visitedStages.LastOrDefault();

		public bool CanGoBack => PreviousStage is not null;

		void IFlowContextChangeable.Reset()
		{
			ErrorMessage = null;
		}

		void IFlowContextChangeable.SetCurrentStage(FlowStageIdentifier newStage)
		{
			ArgumentNullException.ThrowIfNull(newStage);

			if (newStage == CurrentStage)
			{
				return;
			}

			// Returning to the stage we came from is going back, so it leaves the history
			if (newStage == PreviousStage)
			{
				_visitedStages.RemoveAt(_visitedStages.Count - 1);
			}
			else if (CurrentStage is not null)
			{
				_visitedStages.Add(CurrentStage);
			}

			CurrentStage = newStage;
		}
	}
}
