namespace SearchAlgorithms.Model.Containers
{
    class Stack<T> : Interfaces.IAdapter<T>
    {
        // FIELDS
        System.Collections.Generic.Stack<T> stack;
        // CONSTRUCTORS
        public Stack()
        {
            stack = new System.Collections.Generic.Stack<T>();
        }
        // PROPERTIES
        public int Size => stack.Count;
        // METHODS
        public void Add(T item) => stack.Push(item);
        public void Remove() => stack.Pop();
        public T Peek() => stack.Peek();
        public bool IsEmpty() => stack.Count == 0;
        public void Clear() => stack.Clear();
    }
}
