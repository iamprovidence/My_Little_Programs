using TicketApi.Contracts;

namespace DisplayTextWorker.Host.Jobs
{
    public class TestConnectionJob : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<TestConnectionJob> _logger;

        public TestConnectionJob(IServiceScopeFactory serviceScopeFactory, ILogger<TestConnectionJob> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var ticketApiClient = scope.ServiceProvider.GetRequiredService<ITicketApiClient>();

                    var ticketApiStatus = await ticketApiClient.GetHealthStatus(stoppingToken);

                    var logLevel = ticketApiStatus ? LogLevel.Information : LogLevel.Warning;

                    _logger.Log(logLevel, "Ticket API status: {ticketApiStatus}", ticketApiStatus);
                }

                await Task.Delay(10_000, stoppingToken);
            }
        }
    }
}
