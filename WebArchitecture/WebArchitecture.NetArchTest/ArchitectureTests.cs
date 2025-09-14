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

		/*
	  	// data-driven test
	    [Fact]
	    public void Validate_Cross_Layer_References()
	    {
	        var wrongReferences = new List<(string From, string To)>
	        {
	            ("Domain", "Application"),
	            ("Domain", "Infrastructure"),
	            ("Domain", "Presentation"),
	    
	            ("Application", "Infrastructure"),
	            ("Application", "Presentation"),
	
	            ("Infrastructure", "Presentation"),
	        };
	
	        var location = Assembly.GetExecutingAssembly().Location;
	        var assemblies = Directory
	            .EnumerateFiles(Path.GetDirectoryName(location), "PREFIX*.dll")
	            .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
	            .ToList();
	
	        foreach (var layer in wrongReferences)
	        {
	            foreach (var assembly in assemblies) 
	            {
	                var references = assembly
	                  .GetReferencedAssemblies()
	                  .Where(x => x.FullName.StartsWith("Prefix"));
	
	                foreach (var reference in references) 
	                {
	                    var isReferenceAllowed = 
	                      assembly.FullName.Contains(layer.From) &&
	                      reference.FullName.Contains(layer.To)      
	
	                    Assert.False(isReferenceAllowed, $"Layer [{layer.From}] should not reference [{layer.To}]");
	                }
	            }
	        }
	    }
  		*/
	}
}
