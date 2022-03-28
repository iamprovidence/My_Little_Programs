using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Ductus.FluentDocker.Builders;
using FluentAssertions;
using FunWithTests.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
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
            var composeFile = FileHelper.ReverseGetFile("docker-compose.yml");

            using var docker = new Builder()
                .UseContainer()
                .UseCompose()
                .FromFile(composeFile)
                .RemoveOrphans()
                // .WaitForHttp(...)
                .Build()
                .Start();

            using var client = HttpClientFactory.Create();

            // Act
            var result = await client.GetAsync("http://localhost:5000/api/server/ping");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task InMemory_Get_Ping_ShouldSucceed()
        {
            // Arrange
            using var appFactory = new WebApplicationFactory<Startup>();
            using var client = appFactory.CreateClient();

            // Act
            var result1 = await client.GetAsync("/api/server/ping");
            var result2 = await client.GetAsync("http://localhost:5000/api/server/ping");

            // Assert
            result1.StatusCode.Should().Be(HttpStatusCode.OK);
            result2.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ExternalServer_Get_Ping_ShouldSucceed()
        {
            // Arrange
            using var wireMock = WireMockServer.Start();

            wireMock
                .Given(Request.Create().UsingGet().WithPath("/api/server/ping"))
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK).WithBodyAsJson(new
                {
                    State = "Running...",
                }));

            using var client = HttpClientFactory.Create();
            client.BaseAddress = new Uri(wireMock.Urls.First());

            // Act
            var result = await client.GetAsync("/api/server/ping");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
