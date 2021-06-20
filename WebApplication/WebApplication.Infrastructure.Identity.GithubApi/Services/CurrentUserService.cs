using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Identity.Abstractions;
using WebApplication.Infrastructure.Identity.GitHubApi.Interfaces;

namespace WebApplication.Infrastructure.Identity.GitHubApi.Services
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
