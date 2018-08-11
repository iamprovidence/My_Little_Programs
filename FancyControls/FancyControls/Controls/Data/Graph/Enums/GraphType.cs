using System.Windows.Forms.DataVisualization.Charting;

namespace FancyControls.Data
{
    /// <summary>
    /// Specifies a graph type for a FancyControls.Data.Graph.
    /// </summary>
    public enum GraphType
    {
        /// <summary>
        /// Point graph type.
        /// </summary>
        Point = SeriesChartType.Point,
        /// <summary>
        /// Line graph type.
        /// </summary>
        Line = SeriesChartType.Line,
        /// <summary>
        /// Spline graph type.
        /// </summary>
        Spline = SeriesChartType.Spline,
        /// <summary>
        /// Area graph type.
        /// </summary>
        Area = SeriesChartType.Area,
        /// <summary>
        /// Spline area graph type.
        /// </summary>
        SplineArea = SeriesChartType.SplineArea
    }
}
