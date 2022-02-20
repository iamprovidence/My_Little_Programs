using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using System.Collections.Generic;
using System.Linq;
using WebArchitecture.API;
using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace WebArchitecture.ArchUnitNET
{
	public class ArchitectureTests
	{
		private static readonly Architecture Architecture = new ArchLoader()
			.LoadAssembliesIncludingDependencies(typeof(Startup).Assembly)
			.Build();

		[Fact]
		public void CrossLayerReferences()
		{
			var wrongReferences = new List<(string from, string to)>
			{
				("WebArchitecture.Domain.Entities", "WebArchitecture.Application.Contracts"),

				("WebArchitecture.Application", "WebArchitecture.Infrastructure.ApiClient.Http"),
				("WebArchitecture.Application", "WebArchitecture.Infrastructure.Identity.GitHubApi"),
				("WebArchitecture.Application", "WebArchitecture.Infrastructure.OpenApi.Swagger"),
				("WebArchitecture.Application", "WebArchitecture.Infrastructure.Persistence.MsSql"),

				("WebArchitecture.API", "WebArchitecture.Application.ApiClient.Abstractions"),
				("WebArchitecture.API", "WebArchitecture.Application.Identity.Abstractions"),
				("WebArchitecture.API", "WebArchitecture.Application.Persistence.Abstractions"),
				("WebArchitecture.API", "WebArchitecture.Domain.Entities"),

				("WebArchitecture.Infrastructure.ApiClient.Http", "WebArchitecture.Application.UseCases"),
			};

			foreach (var (layer, wrongReference) in wrongReferences)
			{
				var applicationRule = Types()
					.That()
					.ResideInAssembly(layer)
					.Should()
					.NotDependOnAny(Types().That().ResideInAssembly(wrongReference))
					.Because("it is forbidden");

				applicationRule.Check(Architecture);
				// Assert.True(applicationRule.HasNoViolations(Architecture), applicationRule.Description);
			}
		}

	}
}
