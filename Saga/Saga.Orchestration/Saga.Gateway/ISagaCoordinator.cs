using System.Threading.Tasks;

namespace Saga.Gateway
{
	internal interface ISagaCoordinator<TRequest, TResponse>
	{
		Task<TResponse> Process(TRequest request);
	}
}
