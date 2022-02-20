using NetArchTest.Rules;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebArchitecture.API;
using Xunit;

namespace WebArchitecture.NetArchTest
{
	public class ArchitectureTests
	{
		// need this to make sure reference exists
		private static readonly Assembly EntryProint = typeof(Startup).Assembly;

		[Fact]
		public void CrossLayerReferences()
		{
			var wrongReferences = new List<(string Namespace, string Dependency)>
			{
				("Domain", "WebArchitecture.Application.Contracts"),

				("Application", "WebArchitecture.Infrastructure.ApiClient.Http"),
				("Application", "WebArchitecture.Infrastructure.Identity.GitHubApi"),
				("Application", "WebArchitecture.Infrastructure.OpenApi.Swagger"),
				("Application", "WebArchitecture.Infrastructure.Persistence.MsSql"),

				("Infrastructure", "WebArchitecture.Application.UseCases"),
			};

			foreach (var reference in wrongReferences)
			{
				var result = Types.InCurrentDomain()
					.That()
					.ResideInNamespaceMatching(reference.Namespace)
					.Should()
					.NotHaveDependencyOn(reference.Dependency)
					.GetResult();

				Assert.True(result.IsSuccessful, string.Join("\n", result.FailingTypeNames ?? Enumerable.Empty<string>()));
			}
		}
	}
}
