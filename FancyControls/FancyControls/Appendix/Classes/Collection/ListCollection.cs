using System.Collections.Generic;

namespace FancyControls
{
    /// <summary>
    /// Represents the list collection of items in a FancyControls.
    /// </summary>
    public class ListCollection<T> :List<T>
    {
        /// <summary>
        /// Initializes a new instance of namespace FancyControls.ListCollection.
        /// </summary>
        public ListCollection()
            :base(10) {  }
        /// <summary>
        /// Initializes a new instance of the FancyControls.ListCollection class that
        /// is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">
        /// The number of elements that the new list can initially store.
        /// </param>
        /// <exception cref="System.AssemblyLoadEventArgs">
        /// Capacity is less than 0
        /// </exception>
        public ListCollection(int capacity) 
            : base(capacity) { }

        /// <summary>
        /// Initializes a new instance of the FancyControls.ListCollection class that
        /// contains elements copied from the specified collection and has sufficient capacity
        /// to accommodate the number of elements copied.
        /// </summary>
        /// <param name="collection">
        /// The collection whose elements are copied to the new list.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// Collection is null.
        /// </exception>
        public ListCollection(IEnumerable<T> collection) 
            : base(collection) { }
    }
}
