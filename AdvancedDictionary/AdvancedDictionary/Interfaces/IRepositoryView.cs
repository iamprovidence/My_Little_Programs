namespace AdvancedDictionary.Interfaces
{
    public interface IRepositoryView<T,C> : IRepository<T,C> where C : IRepositoryCollection<T>
    {
        string Name { get; }
    }
}
