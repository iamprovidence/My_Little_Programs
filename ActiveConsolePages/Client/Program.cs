using ActiveConsolePages;
using Client.Domain;
using Client.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleHost
                .CreateDefaultBuilder()
                .ConfigureHost(hostConfig =>
                {
                    hostConfig.EnableBreadcrumbs = true;
                })
                .ConfigureServices(services =>
                {
                    services.AddScoped<Calculator>();
                })
                .SetEntryPoint<MainPage>()
                .Build()
                .Run();
        }
    }
}
