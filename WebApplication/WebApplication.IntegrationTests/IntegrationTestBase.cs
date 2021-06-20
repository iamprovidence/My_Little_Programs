using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using WebApplication.API;
using WebApplication.Application.Persistence.Abstractions;
using WebApplication.Infrastructure.ApiClient.Http.Services;
using WebApplication.IntegrationTests.GitHubApiHelpers;
using WebApplication.IntegrationTests.PersistenceHelpers;
using WireMock.Server;

namespace WebApplication.IntegrationTests
{
	public abstract class IntegrationTestBase : IDisposable
	{
		protected readonly IApplicationDbContext _dbContext;
		protected readonly WireMockServer _gitHubApiServer;

		protected WebApplicationFactory<Startup> _webAppFactory;
		protected HttpClient _httpClient;

		protected IntegrationTestBase()
		{
			_dbContext = InMemoryDbContextFactory.BuildDbContext();

			_gitHubApiServer = FakeGitHubApiServer.Create();

			_webAppFactory = null;
			_httpClient = null;
		}

		public void Dispose()
		{
			_gitHubApiServer.Stop();
			_gitHubApiServer.Dispose();

			(_dbContext as IDisposable)?.Dispose();

			_webAppFactory.Dispose();
			_httpClient.Dispose();
		}

		protected WebServerBuilder GetTestServerBuilder()
		{
			return new WebServerBuilder(this);
		}

		public class WebServerBuilder
		{
			private readonly IntegrationTestBase _integrationTestBase;

			public WebServerBuilder(IntegrationTestBase integrationTestBase)
			{
				_integrationTestBase = integrationTestBase;
			}

			internal WebServerBuilder MockData<T>(params T[] entities)
				where T : class
			{
				_integrationTestBase._dbContext.MockDbSet(entities);

				return this;
			}

			internal WebApplicationHttpClient BuildServer()
			{
				_integrationTestBase._webAppFactory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
				{
					builder
						.ConfigureAppConfiguration((webHostBuilderContext, configurationBuilder) =>
						{
							configurationBuilder.AddInMemoryCollection(new KeyValuePair<string, string>[]
							{
								FakeGitHubApiServer.GetConfiguration(_integrationTestBase._gitHubApiServer),
							});
						})
						.ConfigureServices(services =>
						{
							services.RemoveAll(typeof(IApplicationDbContext));

							services.AddScoped(f => _integrationTestBase._dbContext);
						});
				});
				_integrationTestBase._httpClient = _integrationTestBase._webAppFactory.CreateClient();

				return new WebApplicationHttpClient(_integrationTestBase._httpClient);
			}
		}
	}
}
