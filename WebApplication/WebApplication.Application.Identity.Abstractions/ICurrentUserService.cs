using System.Threading;
using System.Threading.Tasks;

namespace WebApplication.Application.Identity.Abstractions
{
	public interface ICurrentUserService
	{
		Task<string> GetUserName(CancellationToken cancellationToken = default);
	}
}
