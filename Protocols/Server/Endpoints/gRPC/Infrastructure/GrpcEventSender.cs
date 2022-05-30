using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Server.Application.Common;

namespace Server.Endpoints.gRPC.Infrastructure
{
    public class GrpcEventSender : IEventSender, IGrpcEventQueue
    {
        private readonly Channel<EventArgs> _queue;

        public GrpcEventSender()
        {
            var options = new BoundedChannelOptions(capacity: 4)
            {
                FullMode = BoundedChannelFullMode.Wait
            };
            _queue = Channel.CreateBounded<EventArgs>(options);
        }

        public async Task Publish(EventArgs eventArgs, CancellationToken cancellationToken)
        {
            await _queue.Writer.WriteAsync(eventArgs, cancellationToken);
        }

        public async Task<EventArgs> GetEvent(CancellationToken cancellationToken)
        {
            return await _queue.Reader.ReadAsync(cancellationToken);
        }
    }
}
