namespace FancyControls
{
    /// <summary>
    /// Represents the class that contain event data: index of item
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(true)]
    public class IndexEventArgs: System.EventArgs
    {
        // FIELDS
        int index;

        // PROPERTIES
        /// <summary>
        /// Gets an index of element.
        /// </summary>
        public int Index => index;

        // CONSTRUCTORS
        /// <summary>
        ///  Initializes a new instance of the FancyControls.IndexEventArgs class.
        /// </summary>
        /// <param name="index">
        /// The index of element.
        /// </param>
        public IndexEventArgs(int index)
        {
            this.index = index;
        }
    
    }
}
