using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Client.Contracts;
using Client.Contracts.ViewModels.AddPlatform;

namespace Client
{
    internal static class ApiClientTest
    {
        public static async Task Run<TApiClient>(string protocolName)
            where TApiClient : IApiClient, new()
        {
            Console.WriteLine($"--- {protocolName} ---");
            var timer = Stopwatch.StartNew();

            var client = new TApiClient();

            // subscription
            using var _ = client.SubscribeToAddPlatformStream((addedPlatform) =>
            {
                Console.WriteLine($"Created platform from event [ID:{addedPlatform.Id} Name:{addedPlatform.Name}]");
            }, CancellationToken.None);

            // create
            var result = await client.AddPlatform(new AddPlatformRequest
            {
                Name = "Docker",
            }, CancellationToken.None);
            Console.WriteLine($"Created platform [ID:{result.Id} Name:{result.Name}]");

            // get
            Console.WriteLine("Platforms:");
            var platforms = await client.GetPlatforms();
            foreach (var platform in platforms)
            {
                Console.WriteLine($" - [ID:{platform.Id} Name:{platform.Name}]");
            }

            Console.WriteLine("Commands:");
            var commands = await client.GetCommands();
            foreach (var command in commands)
            {
                Console.WriteLine($" - [ID:{command.Id} CommandLine:{command.CommandLine}]");
            }

            timer.Stop();
            Console.WriteLine($"Time taken: {timer.Elapsed}");

            Console.WriteLine();
        }
    }
}
