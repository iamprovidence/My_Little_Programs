namespace AdvancedDictionary.Interfaces
{
    public interface IDescriptionWordList<T>:IDescriptionWordView<T>
    {
        void Add(T item);
        void AddRange(System.Collections.Generic.IEnumerable<T> items);
        void Replace(T oldValue, T newValue);
        void Remove(T item);
        void Clear();
    }
}
