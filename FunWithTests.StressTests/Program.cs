using System;
using System.Net.Http;
using NBomber.Contracts;
using NBomber.CSharp;

namespace FunWithTests.StressTests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var client = HttpClientFactory.Create();

            var step = Step.Create("step", async context =>
            {
                var response = await client.GetAsync("http://localhost:5000/api/server/time", context.CancellationToken);

                return response.IsSuccessStatusCode
                    ? Response.Ok(statusCode: (int)response.StatusCode)
                    : Response.Fail(statusCode: (int)response.StatusCode);
            });

            var scenario = ScenarioBuilder
                .CreateScenario("http_load", step)
                .WithWarmUpDuration(TimeSpan.FromSeconds(5))
                .WithLoadSimulations(Simulation.InjectPerSec(rate: 100, during: TimeSpan.FromSeconds(5)));
            //.WithLoadSimulations(Simulation.KeepConstant(copies: 15, during: TimeSpan.FromSeconds(5)));

            NBomberRunner
                .RegisterScenarios(scenario)
                .Run();
        }
    }
}
