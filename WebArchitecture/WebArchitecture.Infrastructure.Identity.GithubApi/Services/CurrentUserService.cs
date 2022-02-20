using System.Threading;
using System.Threading.Tasks;
using WebArchitecture.Infrastructure.Identity.GitHubApi.Interfaces;
using WebArchitecture.Application.Identity.Abstractions;

namespace WebArchitecture.Infrastructure.Identity.GitHubApi.Services
{
	internal class CurrentUserService : ICurrentUserService
	{
		private readonly IGitHubApiHttpClient _gitHubClient;

		public CurrentUserService(IGitHubApiHttpClient gitHubClient)
		{
			_gitHubClient = gitHubClient;
		}

		public async Task<string> GetUserName(CancellationToken cancellationToken = default)
		{
			const string userName = "iamprovidence";

			var gitHubUser = await _gitHubClient.GetUser(userName, cancellationToken);

			return gitHubUser.Name;
		}
	}
}
