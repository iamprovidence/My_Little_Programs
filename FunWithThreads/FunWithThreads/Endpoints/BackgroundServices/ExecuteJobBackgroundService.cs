using FunWithThreads.Application;

namespace FunWithThreads.Endpoints.BackgroundServices
{
    public class ExecuteJobBackgroundService : BackgroundService
    {
        private readonly JobService _jobService;

        public ExecuteJobBackgroundService(JobService jobService)
        {
            _jobService = jobService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var job = await _jobService.Dequeue(stoppingToken);

                    await job.Execute(stoppingToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
