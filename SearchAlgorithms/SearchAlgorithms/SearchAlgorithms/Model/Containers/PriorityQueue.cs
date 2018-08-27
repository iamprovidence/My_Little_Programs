namespace SearchAlgorithms.Model.Containers
{
    class PriorityQueue<T> : Interfaces.IAdapter<T>
    {
        // FIELDS
        System.Collections.Generic.SortedSet<T> priorityQueue;
        // CONSTRUCTORS
        public PriorityQueue()
        {
            priorityQueue = new System.Collections.Generic.SortedSet<T>();
        }
        public PriorityQueue(System.Collections.Generic.IComparer<T> comparer)
        {
            priorityQueue = new System.Collections.Generic.SortedSet<T>(comparer);
        }
        // PROPERTIES
        public int Size => priorityQueue.Count;
        // METHODS
        public void Add(T item) => priorityQueue.Add(item);
        public void Remove() => priorityQueue.Remove(priorityQueue.Min);
        public T Peek() => priorityQueue.Min;
        public bool IsEmpty() => priorityQueue.Count == 0;
        public void Clear() => priorityQueue.Clear();
    }
}
