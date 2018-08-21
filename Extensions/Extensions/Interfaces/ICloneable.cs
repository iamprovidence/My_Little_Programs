namespace Extensions
{
    /// <summary>
    /// Supports cloning, which creates a new instance of a class with the same value as an existing instance.
    /// </summary>
    /// <typeparam name="T">The value type</typeparam>
    public interface ICloneable<out T> : System.ICloneable
    {
        /// <summary>
        /// Creates an copy of the current instance.    
        /// </summary>
        /// <returns>
        /// A copy of this instance.
        /// </returns>
        new T Clone();
    }
}
