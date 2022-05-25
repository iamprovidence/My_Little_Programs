namespace FunWithThreads.Application
{
    public class LogTimeJob : IJob
    {
        private readonly ILogger<LogTimeJob> _logger;

        public LogTimeJob(ILogger<LogTimeJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"--- Time: {DateTimeOffset.Now} ---");

            return Task.CompletedTask;
        }
    }
}
