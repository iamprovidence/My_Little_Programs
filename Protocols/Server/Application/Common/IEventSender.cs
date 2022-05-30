using System;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Application.Common
{
    public interface IEventSender
    {
        Task Publish(EventArgs eventArgs, CancellationToken cancellationToken);
    }
}
