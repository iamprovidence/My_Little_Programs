namespace SearchAlgorithms.Model.Interfaces
{
    interface IAdapter<T>
    {
        void Add(T item);
        void Remove();
        T Peek();
        bool IsEmpty();
        int Size { get; }
        void Clear();
    }
}
