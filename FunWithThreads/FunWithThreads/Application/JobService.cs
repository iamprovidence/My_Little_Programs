using System.Threading.Channels;

namespace FunWithThreads.Application
{
    public class JobService
    {
        private readonly Channel<IJob> _queue;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public JobService(IServiceScopeFactory serviceScopeFactory)
        {
            // Capacity should be set based on the expected application load and
            // number of concurrent threads accessing the queue.
            // 
            // BoundedChannelFullMode.Wait will cause calls to WriteAsync() to return a task,
            // which completes only when space became available. This leads to backpressure,
            // in case too many publishers/calls start accumulating.
            var options = new BoundedChannelOptions(capacity: 4)
            {
                FullMode = BoundedChannelFullMode.Wait
            };
            _queue = Channel.CreateBounded<IJob>(options);

            _serviceScopeFactory = serviceScopeFactory;
        }

        public async ValueTask QueueJob<TJob>(CancellationToken cancellationToken = default)
            where TJob : IJob
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var job = scope.ServiceProvider.GetRequiredService<TJob>();

                await _queue.Writer.WriteAsync(job, cancellationToken);
            }
        }

        public async Task<IJob> Dequeue(CancellationToken cancellationToken = default)
        {
            return await _queue.Reader.ReadAsync(cancellationToken);
        }
    }
}
