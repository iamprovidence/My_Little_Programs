using FlowStage.Models;

namespace FlowStage.Interfaces
{
	public interface IFlowContext
	{
		FlowStageIdentifier CurrentStage { get; }

		FlowStageIdentifier PreviousStage { get; }

		bool CanGoBack { get; }

		string ErrorMessage { get; set; }
	}

	public interface IFlowContextChangeable : IFlowContext
	{
		void Reset();
		void SetCurrentStage(FlowStageIdentifier newStage);
	}
}
