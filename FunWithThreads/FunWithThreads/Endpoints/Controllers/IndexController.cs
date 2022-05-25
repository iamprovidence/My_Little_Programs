using System.Threading.Tasks.Dataflow;
using FunWithThreads.Application;
using Microsoft.AspNetCore.Mvc;

namespace FunWithThreads.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IndexController : ControllerBase
    {
        private readonly ILogger<IndexController> _logger;
        private readonly JobService _jobService;
        private readonly ActionBlock<DateTimeOffset> _requestsQueue;

        public IndexController(ILogger<IndexController> logger, JobService jobService)
        {
            _logger = logger;
            _jobService = jobService;
            _requestsQueue = new ActionBlock<DateTimeOffset>(LogTime, new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = 2,
            });
        }

        [HttpGet]
        public async Task Get()
        {
            // _requestsQueue.Post(DateTimeOffset.Now);
            // _requestsQueue.AsObserver().OnNext(DateTimeOffset.Now);
            await _requestsQueue.SendAsync(DateTimeOffset.Now);

            await _jobService.QueueJob<LogTimeJob>();
        }

        private void LogTime(DateTimeOffset obj)
        {
            _logger.LogWarning($"--- Time: {obj} ---");
        }
    }
}