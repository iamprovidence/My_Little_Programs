using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.Extensions.Logging;
using Server.Application.Common;

namespace Server.Application.Platforms
{
    public class LogTimeJob : IJob
    {
        private readonly ILogger<LogTimeJob> _logger;
        private readonly ActionBlock<DateTimeOffset> _requestsQueue;

        public LogTimeJob(ILogger<LogTimeJob> logger)
        {
            _logger = logger;

            _requestsQueue = new ActionBlock<DateTimeOffset>(LogTime, new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = 2,
            });
        }

        public Task Execute(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"--- Time: {DateTimeOffset.Now} ---");

            // _requestsQueue.Post(DateTimeOffset.Now);
            // _requestsQueue.AsObserver().OnNext(DateTimeOffset.Now);
            // await _requestsQueue.SendAsync(DateTimeOffset.Now);

            return Task.CompletedTask;
        }

        private void LogTime(DateTimeOffset obj)
        {
            _logger.LogWarning($"--- Time: {obj} ---");
        }
    }
}
