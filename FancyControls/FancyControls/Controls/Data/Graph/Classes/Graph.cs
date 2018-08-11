using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace FancyControls.Data
{
    /// <summary>
    /// Represent graph for FancyControls.Data.glGraph.
    /// </summary>
    public class Graph
    {
        /// <summary>
        /// Represent point of FancyControls.Data.Graph.
        /// </summary>
        public struct Point
        {
            double x;
            double y;

            /// <summary>
            /// Gets the value of a point on X axis.
            /// </summary>
            public double X => x;
            /// <summary>
            /// Gets the value of a point on Y axis.
            /// </summary>
            public double Y => y;

            /// <summary>
            /// Initialize a new instance of FancyControls.Data.Graph.Point.
            /// </summary>
            /// <param name="X">
            /// Sets the value of a point on X axis.
            /// </param>
            /// <param name="Y">
            /// Sets the value of a point on Y axis.
            /// </param>
            public Point(double X, double Y)
            {
                this.x = X;
                this.y = Y;
            }
        }
        // FIELDS
        Series graph;
        bool pointValueToolTipShowed;
        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of FancyControls.Data.Graph.
        /// </summary>
        /// <param name="name">
        /// Sets the name of the graph.
        /// </param>
        /// <param name="type">
        /// Sets the graph type.
        /// </param>
        public Graph(string name, GraphType type = GraphType.Line)
        {
            graph = new Series(name);
            graph.ChartType = (SeriesChartType)type;
            pointValueToolTipShowed = false;
        }
        // PROPERTIES
        internal Series GetGraphConfig
        {
            get
            {
                graph.Sort(PointSortOrder.Ascending, "X");
                return graph;
            }
        }
        /// <summary>
        /// Gets or sets whether or not show point value in tooltip.
        /// </summary>
        public bool IsShownPointValue
        {
            get
            {
                return pointValueToolTipShowed;
            }
            set
            {
                pointValueToolTipShowed = value;

                this.graph.ToolTip = pointValueToolTipShowed ? "X = #VALX{#0.0##}, Y = #VALY{#0.0##}" : "";
            }
        }
        /// <summary>
        /// Gets unmodified array of graph point.
        /// </summary>
        public Point[] Points
        {
            get
            {
                return graph.Points.Select(point => new Point(point.XValue,point.YValues.First())).ToArray();
            }
        }
        /// <summary>
        /// Gets or sets graph line color.
        /// </summary>
        public Color LineColor
        {
            get
            {
                return graph.Color;
            }
            set
            {
                graph.Color = value;
            }
        }
        /// <summary>
        /// Gets or sets graph line width.
        /// </summary>
        public int LineWidth
        {
            get
            {
                return graph.BorderWidth;
            }
            set
            {
                graph.BorderWidth = value;
            }
        }
        /// <summary>
        /// Gets or sets a flag that indicated whether the graph is shown in the legend.
        /// </summary>
        public bool IsVisibleInLegend
        {
            get
            {
                return graph.IsVisibleInLegend;
            }
            set
            {
                graph.IsVisibleInLegend = value;
            }
        }
        /// <summary>
        /// Gets or sets tooltip for legend.
        /// </summary>
        public string LegendToolTip
        {
            get
            {
                return graph.LegendToolTip;
            }
            set
            {
                graph.LegendToolTip = value;
            }
        }
        /// <summary>
        /// Gets or sets graph line style.
        /// </summary>
        public ChartDashStyle LineDashStyle
        {
            get
            {
                return graph.BorderDashStyle;
            }
            set
            {
                graph.BorderDashStyle = value;
            }
        }
        /// <summary>
        /// Gets or sets graph name.
        /// </summary>
        public string Name
        {
            get
            {
                return graph.Name;
            }
            set
            {
                graph.Name = value;
            }
        }

        // METHODS
        /// <summary>
        /// Adds point to graph's point collection.
        /// </summary>
        /// <remarks>
        /// For the purpose of set only label or marker use named parameters.
        /// </remarks>
        /// <example>
        /// <code>
        /// .AddPoint(2, 3);
        /// .AddPoint(2, 3, marker: new FancyControls.Data.Marker(){});
        /// .AddPoint(2, 3, label: new FancyControls.Data.Label(){});
        /// </code>
        /// </example>
        /// <param name="x">
        /// An X value of the point.
        /// </param>
        /// <param name="y">
        /// An Y value of the point.
        /// </param>
        /// <param name="marker">
        /// Determine special view for point.
        /// An instance of FancyControls.Data.Marker class.
        /// </param>
        /// <param name="label">
        /// Determine special label for point.
        /// An instance of FancyControls.Data.Label class.
        /// </param>
        /// <returns>
        /// Returns an instance of this graph after point was added.
        /// </returns>
        public Graph AddPoint(double x, double y, Marker marker = null, Label label = null)
        {
            graph.Points.AddXY(x, y);

            if(marker != null)
            {
                marker.Set(graph.Points.Last());
            }
            if(label != null)
            {
                label.Set(graph.Points.Last());
            }
            return this;
        }
        /// <summary>
        /// Adds points to graph's point collection.
        /// </summary>
        /// <remarks>
        /// For the purpose of set only label or marker use named parameters.
        /// </remarks>
        /// <example>
        /// <code>
        /// .AddPoints(new double[2] { -2.3, +2.3 }, new double[2] { 2.3, -2.3 });
        /// .AddPoints(new double[2] { -2.3, +2.3 }, new double[2] { 2.3, -2.3 }, marker: new FancyControls.Data.Marker(){});
        /// .AddPoints(new double[2] { -2.3, +2.3 }, new double[2] { 2.3, -2.3 }, label: new FancyControls.Data.Label(){});
        /// </code>
        /// </example>
        /// <param name="x">
        /// An array of X values of the points.
        /// </param>
        /// <param name="y">
        /// An array of Y values of the points.
        /// </param>
        /// <param name="markers">
        /// Determine special view for point.
        /// An instance of FancyControls.Data.Markers class.
        /// </param>
        /// <param name="label">
        /// Determine special label for point.
        /// An instance of FancyControls.Data.Label class.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// The arrays have different length.
        /// -or- 
        /// The array of X values have the same value.
        /// </exception>
        /// <returns>
        /// Returns an instance of this graph after points were added.
        /// </returns>
        public Graph AddPoints(double[] x, double[] y, Markers markers = null, Label label = null)
        {
            x = x.Distinct().ToArray();
            if(x.Length != y.Length)
            {
                throw new ArgumentException($"'{nameof(x)}' and '{nameof(y)}' should be the same lenght.");
            }
            Array.Sort(x, y);
            for(int i = 0; i < x.Length; ++i)
            {
                graph.Points.AddXY(x[i], y[i]);
            }

            if(markers != null)
            {
                markers.Set(graph);
            }
            if (label != null)
            {
                label.Set(graph);
            }

            return this;
        }
        /// <summary>
        /// Adds points to a graph's point collection depending on input parameters.
        /// </summary>
        /// <remarks>
        /// For the purpose of set only label or marker use named parameters.
        /// </remarks>
        /// <example>
        /// <code>
        /// .AddFunction(Math.Sin, -Math.PI, Math.PI, Math.PI / 10.0)
        /// .AddFunction(Math.Cos, -10, 10, markers: new Markers(){})
        /// .AddFunction(Math.Sqrt, 0, 16, 0.1, label: new Label(){})
        /// </code>
        /// </example>
        /// <param name="f">
        /// A function that reflects the value of the X axis on the Y axis.
        /// </param>
        /// <param name="startX">
        /// The start of input parameters for function on X axis.
        /// </param>
        /// <param name="endX">
        /// The end of input parameters for function on X axis.
        /// </param>
        /// <param name="interval">
        /// The interval between dots on X axis.
        /// </param>
        /// <param name="markers">
        /// Determine special view for point.
        /// An instance of FancyControls.Data.Markers class. 
        /// </param>
        /// <param name="label">
        /// Determine special label for point.
        /// An instance of FancyControls.Data.Label class.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// The startX parameter is greater than endX parameter.
        /// -or- 
        /// The endX parameter is less than startX parameter.
        /// -or- 
        /// The interval is less than zero.
        /// </exception>
        /// <returns>
        /// Returns an instance of this graph after points were added.
        /// </returns>
        public Graph AddFunction(Func<double, double> f, double startX, double endX, double interval = 0.1, Markers markers = null, Label label = null)
        {
            if(startX > endX)
            {
                throw new ArgumentException($"Value of '{nameof(startX)}' should be less than value of '{nameof(endX)}'");
            }
            if(endX < startX)
            {
                throw new ArgumentException($"Value of '{nameof(endX)}' should be greater than value of '{nameof(startX)}'");
            }
            if(interval < 0)
            {
                throw new ArgumentException($"Value of '{nameof(interval)}' should be positive.");
            }

            for(double x = startX; x <= endX; x += interval)
            {
                this.AddPoint(x, f(x));
            }

            if (markers != null)
            {
                markers.Set(graph);
            }
            if (label != null)
            {
                label.Set(graph);
            }
            return this;
        }

    }
}
