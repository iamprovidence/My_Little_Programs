using System.Threading;
using System.Threading.Tasks;

namespace Server.Application.Common
{
    public interface IJob
    {
        Task Execute(CancellationToken cancellationToken);
    }
}
