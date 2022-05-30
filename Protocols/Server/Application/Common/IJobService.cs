using System.Threading;
using System.Threading.Tasks;

namespace Server.Application.Common
{
    public interface IJobService
    {
        Task<IJob> Dequeue(CancellationToken cancellationToken = default);
        ValueTask QueueJob<TJob>(CancellationToken cancellationToken = default)
            where TJob : IJob;
    }
}