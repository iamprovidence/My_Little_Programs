namespace DisplayTextWorker.Host.Jobs
{
    public class DiagnosticJob : BackgroundService
    {
        private readonly ILogger<DiagnosticJob> _logger;

        public DiagnosticJob(ILogger<DiagnosticJob> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {Time}\nThreads in pool: {ThreadCount}\nPending work items: {PendingWorkItemCount}",
                    DateTimeOffset.Now, ThreadPool.ThreadCount, ThreadPool.PendingWorkItemCount);

                await Task.Delay(5_000, stoppingToken);
            }
        }
    }
}