namespace FancyControls
{
    /// <summary>
    ///  Represents an interface for selector controls.
    /// </summary>
    public interface ISelectorControl
    {
        /// <summary>
        /// Select an item at specified index.
        /// </summary>
        /// <param name="index">
        /// An index of an item which should be selected.
        /// </param>
        void SelectAt(int index);
        /// <summary>
        /// Unselect an item at specified index.
        /// </summary>
        /// <param name="index">
        /// An index of an item which should be unselected.
        /// </param>
        void UnselectAt(int index);

        /// <summary>
        /// Occurs when the item at specified index is selected.
        /// </summary>
        event System.EventHandler<IndexEventArgs> ItemSelected;
        /// <summary>
        /// Occurs when the item at specified index is unselected.
        /// </summary>
        event System.EventHandler<IndexEventArgs> ItemUnselected;
    }
}
