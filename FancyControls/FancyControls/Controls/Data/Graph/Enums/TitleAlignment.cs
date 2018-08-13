namespace FancyControls.Data
{
    /// <summary>
    /// Specifies the alignment of a text string relative to its layout rectangle.
    /// </summary>  
    public enum TitleAlignment
    {
        /// <summary>
        /// Specifies the text be aligned near the layout. In a left-to-right layout, the
        /// near position is left. In a right-to-left layout, the near position is right.
        /// </summary>
        Near = 0,
        /// <summary>
        /// Specifies that text is aligned in the center of the layout rectangle.
        /// </summary>
        Center = 1,
        /// <summary>
        /// Specifies that text is aligned far from the origin position of the layout rectangle.
        /// In a left-to-right layout, the far position is right. In a right-to-left layout,
        /// the far position is left.
        /// </summary>
        Far = 2,
        /// <summary>
        /// Specifies that text is aligned near the axis arrow.
        /// </summary>
        NearAxisArrow = 3
    }
}
