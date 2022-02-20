using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using WebArchitecture.API;
using Xunit;

namespace WebArchitecture.ArchitectureTests
{
	public class ArchitectureTests
	{
		// need this to make sure reference exists
		private static readonly Assembly EntryProint = typeof(Startup).Assembly;

		[Fact]
		public void CrossLayerReferences()
		{
			var solutionName = "WebArchitecture";

			var wrongReferences = new List<(string From, string To)>
			{
				("Domain", "Application"),

				("Application", "Infrastructure"),

				("API", "Abstractions"),
				("API", "Domain"),

				("Infrastructure", "Application.UseCases"),
			};

			var location = Assembly.GetExecutingAssembly().Location;
			var assemblies = Directory.EnumerateFiles(Path.GetDirectoryName(location), $"{solutionName}*.dll")
				.Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
				.ToList();

			foreach (var layer in wrongReferences)
			{
				foreach (var assembly in assemblies)
				{
					foreach (var reference in assembly.GetReferencedAssemblies().Where(r => r.FullName.Contains(solutionName)))
					{
						Assert.False(assembly.FullName.Contains(layer.From) && reference.FullName.Contains(layer.To),
							$"Cross-layer reference \nfrom '{assembly.GetName().Name}' \nto '{reference.Name}' \nis prohibited\n");
					}
				}
			}
		}

		[Fact]
		public void CrossModuleReferences()
		{
			var solutionName = "WebArchitecture";

			var modules = new List<string>
			{
				"WebArchitecture",
				"Order",
			};

			var location = Assembly.GetExecutingAssembly().Location;
			var assemblies = Directory.EnumerateFiles(Path.GetDirectoryName(location), $"{solutionName}*.dll")
				.Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
				.ToList();

			for (int i = 0; i < modules.Count; i++)
			{
				for (int j = 0; j < modules.Count; j++)
				{
					if (i == j) continue;

					foreach (var assembly in assemblies)
					{
						foreach (var reference in assembly.GetReferencedAssemblies())
						{
							Assert.False(assembly.FullName.Contains(modules[i]) &&
										 reference.FullName.Contains(modules[j]) &&
										 !reference.FullName.Contains("Contracts"),
								$"Cross-context reference \nfrom '{assembly.FullName}' \nto '{reference.FullName}' \nis prohibited\n");
						}
					}
				}
			}
		}
	}
}
