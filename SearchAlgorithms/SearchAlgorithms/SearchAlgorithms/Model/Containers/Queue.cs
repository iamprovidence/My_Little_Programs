namespace SearchAlgorithms.Model.Containers
{
    class Queue<T> : Interfaces.IAdapter<T>
    {
        // FIELDS
        System.Collections.Generic.Queue<T> queue;
        // CONSTRUCTORS
        public Queue()
        {
            queue = new System.Collections.Generic.Queue<T>();
        }
        // PROPERTIES
        public int Size => queue.Count;
        // METHODS
        public void Add(T item) => queue.Enqueue(item);
        public void Remove() => queue.Dequeue();
        public T Peek() => queue.Peek();
        public bool IsEmpty() => queue.Count == 0;
        public void Clear() => queue.Clear();  
    }
}
