namespace FancyControls
{
    /// <summary>
    /// Represents the class that contain event data: old and new values of properties.
    /// </summary>
    /// <typeparam name="T">
    /// The element type of the property.
    /// </typeparam>
    [System.Runtime.InteropServices.ComVisible(true)]
    public class ValueEventArgs<T>: System.EventArgs
    {
        // FIELDS
        T oldValue;
        T newValue;

        // PROPERTIES
        /// <summary>
        /// Gets an old value of a property.
        /// </summary>
        public T OldValue => oldValue;
        /// <summary>
        /// Gets a new value of a property.
        /// </summary>
        public T NewValue => newValue;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of the FancyControls.ValueEventArgs class.
        /// </summary>
        /// <param name="oldValue">
        /// Sets an old value of a propery, that had been changed.
        /// </param>
        /// <param name="newValue">
        /// Sets a new value of a property, that had been changed.
        /// </param>
        public ValueEventArgs(T oldValue, T newValue)
        {
            this.oldValue = oldValue;
            this.newValue = newValue;
        }
    }
}
