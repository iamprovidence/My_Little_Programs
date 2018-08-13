using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace FancyControls.Data
{
    /// <summary>
    /// Represent axis for FancyControls.Data.glGraph.
    /// </summary>
    public class Axis
    {
        // FIELDS
        System.Windows.Forms.DataVisualization.Charting.Axis axis;
        TitleAlignment titleAlignment;
        string text;
        // CONSTRUCTORS

        // <summary>
        // Initializes a new instance of the FancyControls.Data.Axis class.
        // </summary>
        // <param name="axis">
        // An axis to config.
        // </param>
        internal Axis(System.Windows.Forms.DataVisualization.Charting.Axis axis)
        {
            this.axis = axis;
            this.text = null;
            this.titleAlignment = (TitleAlignment)axis.TitleAlignment;

            this.ArrowStyle = AxisArrowStyle.Lines;
            this.axis.Crossing = 0;
            this.GridLineWidth = 0;
        }
        
        // PROPERTIES

        internal string TextToDisplay
        {
            get
            {
                if (titleAlignment == TitleAlignment.NearAxisArrow)
                {
                    return text;
                }
                return axis.Title;
            }
        }
        /// <summary>
        /// Gets or sets the arrow style of a axis.
        /// </summary>
        /// <returns>
        /// An System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle enumeration value.
        /// </returns>
        public AxisArrowStyle ArrowStyle
        {
            get
            {
                return axis.ArrowStyle;
            }
            set
            {
                axis.ArrowStyle = value;
            }
        }
        /// <summary>
        /// Gets or sets the color of axis.
        /// </summary>
        /// <returns>
        /// A System.Drawing.Color object that represents the color of axis.
        /// </returns>
        public Color Color
        {
            get
            {
                return axis.LineColor;
            }
            set
            {
                axis.LineColor = value;
            }
        }
        /// <summary>
        ///  Gets or sets the interval of an axis.
        /// </summary>
        /// <returns>
        ///  A double value that represents the interval of an axis. 
        ///  The default value is "Auto", which is represented by a value of zero (0).
        /// </returns>
        public double Interval
        {
            get
            {
                return axis.Interval;
            }
            set
            {
                axis.Interval = value;
            }
        }

        /// <summary>
        /// Gets or sets a flag that determines if a fixed number of intervals is used on the axis,
        /// or if the number of intervals depends on the axis size.
        /// </summary>
        /// <returns>
        /// An System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode enumeration value. 
        /// The default value is System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.FixedCount.
        /// </returns>
        public IntervalAutoMode IntervalAutoMode
        {
            get
            {
                return axis.IntervalAutoMode;
            }
            set
            {
                axis.IntervalAutoMode = value;
            }
        }
  
        /// <summary>
        /// Gets or sets the interval offset of an axis.
        /// </summary>
        /// <returns>
        /// A double value that represents the interval offset of an axis. 
        /// The default value is "Auto", which is represented by a value of zero (0).
        /// </returns>
        public double IntervalOffset
        {
            get
            {
                return axis.IntervalOffset;
            }
            set
            {
                axis.IntervalOffset = value;
            }
        }


        /// <summary>
        /// Gets or sets the line style of an axis.
        /// </summary>
        public ChartDashStyle LineDashStyle
        {
            get
            {
                return axis.LineDashStyle;
            }
            set
            {
                axis.LineDashStyle = value;
            }
        }
        /// <summary>
        /// Gets or sets the line width of an axis, in pixels.
        /// </summary>
        public int LineWidth
        {
            get
            {
                return axis.LineWidth;
            }
            set
            {
                axis.LineWidth = value;
            }
        }


        /// <summary>
        /// Gets or sets the maximum value of an axis.
        /// </summary>
        public double Maximum
        {
            get
            {
                return axis.Maximum;
            }
            set
            {
                axis.Maximum = value;
            }
        }

        /// <summary>
        /// Gets or sets the minimum value of an axis.
        /// </summary>
        public double Minimum
        {
            get
            {
                return axis.Minimum;
            }
            set
            {
                axis.Minimum = value;
            }
        }
        /// <summary>
        /// Gets or sets grid line width in all axis.
        /// </summary>
        public int GridLineWidth
        {
            get
            {
                return axis.MajorGrid.LineWidth;
            }
            set
            {
                axis.MajorGrid.LineWidth = value;
            }
        }
        /// <summary>
        /// Gets or sets grid line color in all axis.
        /// </summary>
        public Color GridLineColor
        {
            get
            {
                return axis.MajorGrid.LineColor;
            }
            set
            {
                axis.MajorGrid.LineColor = value;
            }
        }
        /// <summary>
        /// Gets or sets grid dash style in all axis.
        /// </summary>
        public ChartDashStyle GridDashStyle
        {
            get
            {
                return axis.MajorGrid.LineDashStyle;
            }
            set
            {
                axis.MajorGrid.LineDashStyle = value;
            }
        }


        /// <summary>
        /// Gets or sets the orientation of the text in the axis title.
        /// </summary>
        public TextOrientation TextOrientation
        {
            get
            {
                return axis.TextOrientation;
            }
            set
            {
                axis.TextOrientation = value;
            }
        }

        /// <summary>
        /// Gets or sets the title of the axis.
        /// </summary>
        public string Title
        {
            get
            {
                return axis.Title;
            }
            set
            {
                if(titleAlignment == TitleAlignment.NearAxisArrow)
                {
                    text = value;
                }
                else axis.Title = value;
            }
        }

        /// <summary>
        /// Gets or sets the alignment of an axis title.
        /// </summary>
        public TitleAlignment TitleAlignment
        {
            get
            {

                return this.titleAlignment;
            }
            set
            {
                this.titleAlignment = value;

                if (value == TitleAlignment.NearAxisArrow)
                {
                    this.text = axis.Title;
                    axis.Title = "";
                }
                else
                {
                    axis.Title = string.IsNullOrWhiteSpace(text) ? axis.Title : text;
                    text = null;
                    axis.TitleAlignment = (StringAlignment)value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the title font properties of an axis.
        /// </summary>
        public Font TitleFont
        {
            get
            {
                return axis.TitleFont;
            }
            set
            {
                axis.TitleFont = value;
            }
        }

        /// <summary>
        /// Gets or sets the text color of the axis title.
        /// </summary>
        public Color TitleForeColor
        {
            get
            {
                return axis.TitleForeColor;
            }
            set
            {
                axis.TitleForeColor = value;
            }
        }

        // METHODS

        /// <summary>
        /// Converts an axis value to a relative position (0-100%). 
        /// If an axis is logarithmic, the value is converted to a linear scale.
        /// </summary>
        /// <param name="axisValue">
        /// The axis value.
        /// </param>
        /// <returns>
        /// A double value that represents the relative position (0-100%).
        /// </returns>
        public double GetPosition(double axisValue)
        {
            return axis.GetPosition(axisValue);
        }

 
        /// <summary>
        /// Converts an absolute pixel position along an axis to an axis value. 
        /// This method only works in paint events.
        /// </summary>
        /// <param name="position">
        /// The pixel position.
        /// </param>
        /// <returns>
        /// A double value that represents the axis value.
        /// </returns>
        public double PixelPositionToValue(double position)
        {
            return axis.PixelPositionToValue(position);
        }
  
        /// <summary>
        /// Converts a relative coordinate along an axis to an axis value. 
        /// This method only works in paint events.
        /// </summary>
        /// <param name="position">
        /// The relative position (0-100%).
        /// </param>
        /// <returns>
        /// A double value that represents the axis value.
        /// </returns>
        public double PositionToValue(double position)
        {
            return axis.PositionToValue(position);
        }
   
        /// <summary>
        /// Automatically rounds axis values.
        /// </summary>
        public void RoundAxisValues()
        {
            axis.RoundAxisValues();
        }
  
        /// <summary>
        /// Converts an axis value to an absolute coordinate along an axis. 
        /// Measured in pixels. 
        /// This method only works in paint events.
        /// </summary>
        /// <param name="axisValue">
        /// The axis value.
        /// </param>
        /// <returns>
        /// A double value that represents the pixel position.
        /// </returns>
        public double ValueToPixelPosition(double axisValue)
        {
            return axis.ValueToPixelPosition(axisValue);
        }

        /// <summary>
        /// Converts an axis value to its relative position (0-100%). 
        /// If an axis has a logarithmic scale, the value is converted to a linear scale. 
        /// This method only works in paint events.
        /// </summary>
        /// <param name="axisValue">
        /// The axis value.
        /// </param>
        /// <returns>
        /// A double value that represents the relative position (0-100%).
        /// </returns>
        public double ValueToPosition(double axisValue)
        {
            return axis.ValueToPosition(axisValue);
        }
        /// <summary>
        /// Add line on axis.
        /// </summary>
        /// <param name="position">
        /// Position of the line.
        /// </param>
        /// <param name="width">
        /// Width of the line.
        /// </param>
        /// <param name="color">
        /// Color of the line.
        /// </param>
        /// <param name="style">
        /// Style of line.
        /// </param>
        /// <param name="text">
        /// Text of line.
        /// </param>
        /// <param name="textAlignment">
        /// Text alignment of line.
        /// </param>
        /// <param name="textOrientation">
        /// Text orientation of line.
        /// </param>
        public void AddLine(double position, int width, Color color, ChartDashStyle style = ChartDashStyle.Solid, string text = "", StringAlignment textAlignment = StringAlignment.Center, TextOrientation textOrientation = TextOrientation.Auto)
        {
            axis.StripLines.Add(new StripLine()
            {
                Interval = 0,
                IntervalOffset = position,
                BorderWidth = width,
                BorderColor = color,
                BorderDashStyle = style,

                Text = text,
                TextAlignment = textAlignment,
                TextOrientation = textOrientation,


            });
        }
    }
}
