namespace FunWithThreads.Application
{
    public interface IJob
    {
        Task Execute(CancellationToken cancellationToken);
    }
}
