using System;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Endpoints.gRPC.Infrastructure
{
    public interface IGrpcEventQueue
    {
        Task<EventArgs> GetEvent(CancellationToken cancellationToken);
    }
}