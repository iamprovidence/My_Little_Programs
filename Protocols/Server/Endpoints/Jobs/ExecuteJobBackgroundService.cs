using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Server.Application.Common;

namespace Server.Endpoints.Jobs
{
    public class ExecuteJobBackgroundService : BackgroundService
    {
        private readonly IJobService _jobService;

        public ExecuteJobBackgroundService(IJobService jobService)
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
