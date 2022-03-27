using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Ductus.FluentDocker.Builders;
using FluentAssertions;
using FunWithTests.IntegrationTests.Helpers;
using Xunit;

namespace FunWithTests.IntegrationTests
{
    public class ServerApiTests
    {
        [FactSwitch]
        public async Task Get_Ping_ShouldSucceed()
        {
            // Arrange
            using var client = new FunWithTestsHttpClient();

            // Act
            var result = await client.GetAsync("/api/server/ping");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Docker_Get_Ping_ShouldSucceed()
        {
            // Arrange
            var composeFile = GetDockerComposeFilePath("docker-compose.yml");

            using var docker = new Builder()
                .UseContainer()
                .UseCompose()
                .FromFile(composeFile)
                .RemoveOrphans()
                .Build()
                .Start();

            using var client = HttpClientFactory.Create();

            // Act
            var result = await client.GetAsync("http://localhost:5000/api/server/ping");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private string GetDockerComposeFilePath(string fileName)
        {
            var directory = new DirectoryInfo(Directory.GetCurrentDirectory());

            while (directory != null && !directory.GetFiles(fileName).Any())
            {
                directory = directory.Parent;
            }

            if (directory == null)
            {
                throw new FileNotFoundException("Could not find docker compose file");
            }

            return Path.Combine(directory.FullName, fileName);
        }
    }
}
