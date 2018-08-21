namespace Extensions
{
    /// <summary>
    /// Some extension methods for <see cref="System.String"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a new string in which first ocurrence of a specified Unicode character in this instance are replaced with another specified Unicode character.
        /// </summary>
        /// <param name="s">An instance of <see cref="System.String"/> that has been extended</param>
        /// <param name="oldValue">The Unicode character to be replaced.</param>
        /// <param name="newValue">The Unicode character to replace all occurrences of oldChar.</param>
        /// <returns>
        ///  A string that is equivalent to this instance except that firsy instances of oldChar
        ///  are replaced with newChar. If oldChar is not found in the current instance, the
        ///  method returns the current instance unchanged.
        /// </returns>
        public static string ReplaceFirst(this string s, string oldValue, string newValue)
        {
            int index = s.IndexOf(oldValue);
            if (index != -1) return s.Remove(index, oldValue.Length).Insert(index, newValue);
            else return s;
        }
        
    }
}
