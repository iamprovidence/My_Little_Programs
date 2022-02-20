using System.Threading;
using System.Threading.Tasks;

namespace WebArchitecture.Application.Identity.Abstractions
{
	public interface ICurrentUserService
	{
		Task<string> GetUserName(CancellationToken cancellationToken = default);
	}
}
