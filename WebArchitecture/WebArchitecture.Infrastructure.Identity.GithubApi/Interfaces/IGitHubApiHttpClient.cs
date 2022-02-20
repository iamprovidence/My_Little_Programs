using Refit;
using System.Threading;
using System.Threading.Tasks;
using WebArchitecture.Infrastructure.Identity.GitHubApi.Dtos;

namespace WebArchitecture.Infrastructure.Identity.GitHubApi.Interfaces
{
	internal interface IGitHubApiHttpClient
	{
		[Get("/users/{username}")]
		Task<GitHubUserDto> GetUser(string userName, CancellationToken cancellationToken = default);
	}
}
