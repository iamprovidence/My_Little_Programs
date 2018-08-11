using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace FancyControls.Data
{
    /// <summary>
    /// Represent marker for FancyControls.Data.Graph.
    /// </summary>
    public class Marker
    {
        // FIELDS
        /// <summary>
        /// Gets or sets the marker color.
        /// </summary>
        protected Color color;
        /// <summary>
        /// Gets or sets the marker border color.
        /// </summary>
        protected Color borderColor;
        /// <summary>
        /// Gets or sets the marker border width.
        /// </summary>
        protected int borderWidth;
        /// <summary>
        /// Gets or sets the marker image.
        /// </summary>
        protected string image;
        /// <summary>
        /// Gets or sets the marker image transparent color.
        /// </summary>
        protected Color imageTransparentColor;
        /// <summary>
        /// Gets or sets the marker size.
        /// </summary>
        protected int size;
        /// <summary>
        /// Gets or sets the marker style.
        /// </summary>
        protected MarkerStyle style;

        // PROPERTIES
        /// <summary>
        /// Gets or sets the marker color.
        /// </summary>
        public Color Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }
        /// <summary>
        /// Gets or sets the marker border color.
        /// </summary>
        public Color BorderColor
        {
            get
            {
                return borderColor;
            }

            set
            {
                borderColor = value;
            }
        }
        /// <summary>
        /// Gets or sets the marker border width.
        /// </summary>
        public int BorderWidth
        {
            get
            {
                return borderWidth;
            }

            set
            {
                borderWidth = value;
            }
        }
        /// <summary>
        /// Gets or sets the marker image.
        /// </summary>
        public string Image
        {
            get
            {
                return image;
            }

            set
            {
                image = value;
            }
        }
        /// <summary>
        /// Gets or sets the marker image transparent color.
        /// </summary>
        public Color ImageTransparentColor
        {
            get
            {
                return imageTransparentColor;
            }

            set
            {
                imageTransparentColor = value;
            }
        }
        /// <summary>
        /// Gets or sets the marker size.
        /// </summary>
        public int Size
        {
            get
            {
                return size;
            }

            set
            {
                size = value;
            }
        }
        /// <summary>
        /// Gets or sets the marker style.
        /// </summary>
        public MarkerStyle Style
        {
            get
            {
                return style;
            }

            set
            {
                style = value;
            }
        }

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of the FancyControls.Data.Marker class.
        /// </summary>
        /// <param name="color">
        /// Sets marker color.
        /// </param>
        /// <param name="size">
        /// Sets marker size.
        /// </param>
        /// <param name="style">
        /// Sets marker style.
        /// </param>
        public Marker(MarkerStyle style, Color color, int size)
            : this(style,color, size, color, 0, null, Color.Transparent) { }
        /// <summary>
        /// Initializes a new instance of the FancyControls.Data.Marker class.
        /// </summary>
        /// <param name="color">
        /// Sets marker color.
        /// </param>
        /// <param name="size">
        /// Sets marker size.
        /// </param>
        /// <param name="style">
        /// Sets marker style.
        /// </param>
        /// <param name="borderColor">
        /// Sets marker border color.
        /// </param>
        /// <param name="borderWidth">
        /// Sets marker bordor width.
        /// </param>
        /// <param name="image">
        /// Sets marker image.
        /// </param>
        /// <param name="imageTranparentColor">
        /// Sets marker image transparent color.
        /// </param>
        public Marker(MarkerStyle style, Color color, int size, Color borderColor, int borderWidth, string image, Color imageTranparentColor )
        {
            this.Color = color;
            this.Size = size;
            this.Style = style;
            this.BorderColor = borderColor;
            this.Image = image;
            this.ImageTransparentColor = imageTranparentColor;
        }

        // METHODS
        internal virtual void Set(DataPointCustomProperties element)
        {
            element.MarkerBorderColor = BorderColor;
            element.MarkerBorderWidth = BorderWidth;
            element.MarkerColor = Color;
            element.MarkerImage = Image;
            element.MarkerImageTransparentColor = ImageTransparentColor;
            element.MarkerSize = Size;
            element.MarkerStyle = Style;
        }
    }
}
