using System;
using System.Linq;
using System.Collections.Generic;

namespace Extensions
{
    /// <summary>
    /// Some extension methods for <see cref="System.Linq"/>.
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// Apply an action to all elements in collection.
        /// </summary>
        /// <typeparam name="T">The type of value in the collection.</typeparam>
        /// <param name="enumerable">The instance of <see cref="System.Collections.Generic.IEnumerable{T}"/> that has been extended.</param>
        /// <param name="action">The action that shall be applied to all the elements.</param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T element in enumerable)
            {
                action(element);
            }
        }
        /// <summary>
        /// Shuffles a IEnumerable in O(n) time.
        /// </summary>
        /// <typeparam name="T">The type of value in the collection.</typeparam>
        /// <param name="enumerable">The instance of <see cref="System.Collections.Generic.IEnumerable{T}"/> that has been extended.</param>
        /// <returns>The instance of IEnumerable sorted by random.</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
        {
            Random rand = new Random();
            return enumerable.OrderBy(x => rand.Next());
        }
       
        /// <summary>
        /// Return a copy of an instance IEnumerable.
        /// </summary>
        /// <typeparam name="T">The type of value in the collection.</typeparam>
        /// <param name="enumerable">The instance of <see cref="System.Collections.Generic.IEnumerable{T}"/> that has been extended.</param>
        /// <returns>A copy of an instance IEnumerable.</returns>
        public static IEnumerable<T> Clone<T>(this IEnumerable<T> enumerable) where T : ICloneable
        {
            return enumerable.Select(item => (T)item.Clone());
        }

        /// <summary>
        /// Creates an instance of <see cref="System.Collections.Generic.LinkedList{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of value in the collection.</typeparam>
        /// <param name="enumerable">The instance of <see cref="System.Collections.Generic.IEnumerable{T}"/> that has been extended.</param>
        /// <returns>
        /// An instance of <see cref="System.Collections.Generic.LinkedList{T}"/>.
        /// </returns>
        public static LinkedList<T> ToLinkedList<T>(this IEnumerable<T> enumerable)
        {
            return new LinkedList<T>(enumerable);
        }
    }
}
