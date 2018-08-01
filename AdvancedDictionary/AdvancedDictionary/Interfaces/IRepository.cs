namespace AdvancedDictionary.Interfaces
{
    public interface IRepository<T, C> where C: IRepositoryCollection<T>
    {
        C repository { get; }
    }
}
