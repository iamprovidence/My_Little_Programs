using System.Collections.Generic;
using System.Linq;
using System.Net;
using WebArchitecture.Application.Shared.Constants;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace WebArchitecture.IntegrationTests.GitHubApiHelpers
{
	internal class FakeGitHubApiServer
	{
		public static KeyValuePair<string, string> GetConfiguration(WireMockServer mockServer)
		{
			return new(UrlKeys.GitHubApi, mockServer.Urls.First());
		}

		public static WireMockServer Create()
		{
			var wireMock = WireMockServer.Start();

			var gitHubApiRequest = Request.Create().WithHeader("User-Agent", "request");

			wireMock
				.Given(gitHubApiRequest.UsingGet().WithPath("/users/*"))
				.RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK).WithBodyAsJson(new
				{
					Id = 42,
					Login = "octocat",
					Name = "John Doe",
				}));

			return wireMock;
		}
	}
}
