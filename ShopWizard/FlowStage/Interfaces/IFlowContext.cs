using FlowStage.Models;

namespace FlowStage.Interfaces
{
	public interface IFlowContext
	{
		FlowStageIdentifier CurrentStage { get; }

		string ErrorMessage { get; set; }
	}

	public interface IFlowContextChangeable : IFlowContext
	{
		void Reset();
		void SetCurrentStage(FlowStageIdentifier newStage);
	}
}
