using FlowStage.Models;
using System.Threading.Tasks;

namespace FlowStage.Interfaces
{
	public interface IFlowStart
	{
	}

	/// <summary>
	/// Decides which stage the flow begins with and seeds the data of the stages it skips
	/// </summary>
	public interface IFlowStart<TContext> : IFlowStart
		where TContext : IFlowContext
	{
		Task<FlowStageIdentifier> Start(TContext context);
	}
}
