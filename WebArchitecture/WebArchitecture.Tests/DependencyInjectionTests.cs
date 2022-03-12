using Microsoft.Extensions.DependencyInjection;
using WebArchitecture.API;
using Xunit;

namespace WebArchitecture.Tests
{
	public class DependencyInjectionTests
	{
		[Fact]
		public void Dependency_graph_should_be_valid()
		{
			// Arrange
			var serviceCollection = new ServiceCollection();
			serviceCollection
				.AddMvcCore()
				.AddApplicationPart(typeof(Startup).Assembly)
				.AddControllersAsServices();

			var startup = new Startup(configuration: default);

			// Act
			startup.ConfigureServices(serviceCollection);

			// Assert
			AssertHelper.AssertDependencies("WebArchitecture", serviceCollection);
		}
	}
}
