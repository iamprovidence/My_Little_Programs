using DisplayTextWorker.Application;
using DisplayTextWorker.Host.EventHandlers;
using DisplayTextWorker.Host.Jobs;
using DisplayTextWorker.Infrastructure;
using EventBus.Abstractions;
using EventBus.RabbitMq;
using TicketApi.Contracts;
using TicketApi.Contracts.IntegrationEvents;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.Configure<HostOptions>(hostOptions =>
        {
            hostOptions.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.StopHost;
        });

        services.AddScoped<DisplayTextAppService>();
        services.AddScoped<ITicketApiClient, TicketApiClient>();

        services.AddHttpClient(TicketApiClient.HttpClientName);

        // services.AddGrpcClient<TicketApiClient>((services, options) =>
        // {
        //     options.Address = new Uri("");
        // }).AddInterceptor<GrpcExceptionInterceptor>();
        
        services.AddRabbitMqEventBus(hostContext.Configuration);
        services.AddScoped<TicketCreatedEventHandler>();

        services.AddHostedService<DiagnosticJob>();
        services.AddHostedService<TestConnectionJob>();
    })
    .Build();

LogConfigurations(host);

var eventBus = host.Services.GetRequiredService<IEventBus>();

eventBus.Subscribe<TicketCreatedIntegrationEvent, TicketCreatedEventHandler>();

await host.RunAsync();

static void LogConfigurations(IHost host)
{
    var configuration = host.Services.GetRequiredService<IConfiguration>();

    host.Services.GetRequiredService<ILoggerFactory>().CreateLogger("Host")
        .LogInformation("TicketHttpApi url {TicketHttpApiUrl}\nRabbitMq host {RabbitMqHost}",
            configuration[UrlKey.TicketHttpApi], configuration["ConnectionStrings:RabbitMqHost"]);
}
