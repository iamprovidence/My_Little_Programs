using System.Collections.Generic;

namespace AdvancedDictionary.Interfaces
{
    public interface IRepositoryCollection<T>:IEnumerable<T>
    {
        bool Add(T item);
        void RemoveAt(int index);
        T this[int i]{ set; get; }
        void Save();
    }
}
