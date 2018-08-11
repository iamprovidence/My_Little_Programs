using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace FancyControls.Data
{
    /// <summary>
    /// Represent markers for FancyControls.Data.Graph.
    /// </summary>
    public class Markers : Marker
    {
        // FIELDS
        int step;
        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of the FancyControls.Data.Markers class.
        /// </summary>
        /// <param name="color">
        /// Sets markers color.
        /// </param>
        /// <param name="size">
        /// Sets markers size.
        /// </param>
        /// <param name="style">
        /// Sets markers style.
        /// </param>
        /// <param name="step">
        /// Sets markers step.
        /// 10 step equal to value of 1.
        /// </param>
        public Markers(MarkerStyle style, Color color, int size, int step)
            : base(style, color, size)
        {
            this.Step = step;
        }
        /// <summary>
        /// Initializes a new instance of the FancyControls.Data.Markers class.
        /// </summary>
        /// <param name="color">
        /// Sets markers color.
        /// </param>
        /// <param name="size">
        /// Sets markers size.
        /// </param>
        /// <param name="style">
        /// Sets markers style.
        /// </param>
        /// <param name="step">
        /// Sets markers step.
        /// 10 step equal to value of 1.
        /// </param>
        /// <param name="borderColor">
        /// Sets markers border color.
        /// </param>
        /// <param name="borderWidth">
        /// Sets markers border width.
        /// </param>
        /// <param name="image">
        /// Sets markers image.
        /// </param>
        /// <param name="imageTranparentColor">
        /// Sets markers image transparent color.
        /// </param>
        public Markers(MarkerStyle style, Color color, int size, Color borderColor, int borderWidth, string image, Color imageTranparentColor, int step)
            : base(style, color, size, borderColor, borderWidth, image, imageTranparentColor)
        {
            this.Step = step;
        }

        // PROPERTIES
        /// <summary>
        /// Gets or sets markers step.
        /// 10 step equal to value of 1.
        /// </summary>
        public int Step
        {
            get
            {
                return step;
            }

            set
            {
                step = value;
            }
        }
        // METHODS
        internal override void Set(DataPointCustomProperties element)
        {
            if (element is Series)
            {
                ((Series)element).MarkerStep = Step;
            }
            base.Set(element);
        }
    }
}
