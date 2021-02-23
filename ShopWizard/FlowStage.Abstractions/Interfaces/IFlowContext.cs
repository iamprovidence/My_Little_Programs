namespace FlowStage.Abstractions.Interfaces
{
	public interface IFlowContext
	{
		string StageName { get; }

		string ErrorMessage { get; set; }
	}

	public interface IFlowContextChangeable : IFlowContext
	{
		void Reset();
		void SetStage(string newStage);
	}
}
