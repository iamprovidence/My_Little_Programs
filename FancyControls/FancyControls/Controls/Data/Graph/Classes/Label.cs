using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace FancyControls.Data
{
    /// <summary>
    /// Represent label for FancyControls.Data.Graph.
    /// </summary>
    public class Label
    {
        const string valueText = "X = #VALX{#0.0#}\nY = #VALY{#0.0#}";
        // FIELDS
        bool showValue;
        string text;
        int angle;
        Color backColor;
        Color borderColor;
        ChartDashStyle borderDashStyle;
        int borderWidth;
        Color foreColor;
        string format;
        string toolTip;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of the FancyControls.Data.Label class.
        /// </summary>
        /// <param name="showValue">
        /// Determine if label show point position.
        /// </param>
        public Label(bool showValue)
            : this(showValue? valueText : "", 0, Color.Transparent, Color.Transparent, ChartDashStyle.Solid, 0, Color.Black, "", "")
        {
            this.showValue = showValue;
        }
        /// <summary>
        /// Initializes a new instance of the FancyControls.Data.Label class.
        /// </summary>
        /// <param name="text">
        /// Sets the text of the data point label.
        /// </param>
        public Label(string text)
            : this(text, 0, Color.Transparent, Color.Transparent, ChartDashStyle.Solid, 0, Color.Black, "", "") { }
        /// <summary>
        /// Initializes a new instance of the FancyControls.Data.Label class.
        /// </summary>
        /// <param name="text">
        /// Sets the text of the data point label.
        /// </param>
        /// <param name="angle">
        /// Sets the angle of the data point label.
        /// </param>
        /// <param name="backColor">
        /// Sets the background color of the data point label.
        /// </param>
        /// <param name="borderColor">
        /// Sets the border color of the data point label.
        /// </param>
        /// <param name="borderDashStyle">
        /// Sets the border style of the label.
        /// </param>
        /// <param name="borderWidth">
        /// Sets the border width of the label.
        /// </param>
        /// <param name="foreColor">
        /// Sets the text color of the label.
        /// </param>
        /// <param name="format">
        /// Sets the format of the data point label.
        /// </param>
        /// <param name="toolTip">
        /// Sets the tooltip for the data point label.
        /// </param>
        public Label(string text, int angle, Color backColor, Color borderColor, ChartDashStyle borderDashStyle, int borderWidth, Color foreColor, string format, string toolTip)
        {
            this.showValue = false;

            this.Text = text;
            this.Angle = angle;
            this.BackColor = backColor;
            this.BorderColor = borderColor;
            this.BorderDashStyle = borderDashStyle;
            this.BorderWidth = borderWidth;
            this.ForeColor = foreColor;
            this.Format = format;
            this.ToolTip = toolTip;
        }

        // PROPERTIES
        /// <summary>
        /// Gets or sets the text of the data point label.
        /// </summary>
        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                if(showValue)
                {
                    return;
                }
                text = value;
            }
        }
        /// <summary>
        /// Gets or sets the angle of the data point label.
        /// </summary>
        public int Angle
        {
            get
            {
                return angle;
            }

            set
            {
                angle = value;
            }
        }
        /// <summary>
        /// Gets or sets the background color of the data point label.
        /// </summary>
        public Color BackColor
        {
            get
            {
                return backColor;
            }

            set
            {
                backColor = value;
            }
        }
        /// <summary>
        /// Gets or sets the border color of the data point label.
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
        /// Gets or sets the border style of the label.
        /// </summary>
        public ChartDashStyle BorderDashStyle
        {
            get
            {
                return borderDashStyle;
            }

            set
            {
                borderDashStyle = value;
            }
        }
        /// <summary>
        /// Gets or sets the border width of the label.
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
        /// Gets or sets the text color of the label.
        /// </summary>
        public Color ForeColor
        {
            get
            {
                return foreColor;
            }

            set
            {
                foreColor = value;
            }
        }
        /// <summary>
        /// Gets or sets the format of the data point label.
        /// </summary>
        public string Format
        {
            get
            {
                return format;
            }

            set
            {
                format = value;
                if(showValue)
                {
                    if (System.String.IsNullOrWhiteSpace(format))
                    {
                        text = valueText;
                    }
                    else
                    {
                        text = "X = #VALX{" + format + "}\nY = #VALY{" + format + "}";
                    }
                }
            }
        }
        /// <summary>
        /// Gets or sets the tooltip for the data point label.
        /// </summary>
        public string ToolTip
        {
            get
            {
                return toolTip;
            }

            set
            {
                toolTip = value;
            }
        }

        // METHODS
        internal virtual void Set(DataPointCustomProperties element)
        {
            element.Label = Text;
            element.LabelAngle = Angle;
            element.LabelBackColor = BackColor;
            element.LabelBorderColor = BorderColor;
            element.LabelBorderDashStyle = BorderDashStyle;
            element.LabelBorderWidth = BorderWidth;
            element.LabelForeColor = ForeColor;
            element.LabelFormat = Format;
            element.LabelToolTip = ToolTip;
        }
    }
}
