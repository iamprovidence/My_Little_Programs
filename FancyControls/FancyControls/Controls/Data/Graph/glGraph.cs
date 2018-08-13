using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FancyControls.Data
{
    public partial class glGraph : UserControl
    {
        // FIELDS
        Axis axisX;
        Axis axisY;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of FancyControls.Data.glGraph class.
        /// </summary>
        public glGraph()
        {
            InitializeComponent();

            axisX = new Axis(this.graphArea.ChartAreas["CoordinateSystem"].AxisX);
            axisY = new Axis(this.graphArea.ChartAreas["CoordinateSystem"].AxisY);

            this.graphArea.PostPaint += GraphArea_PostPaint;
        }
        // drawing axis title near arrows
        private void GraphArea_PostPaint(object sender, ChartPaintEventArgs e)
        {
            if (e.ChartElement is Chart && (axisX.TitleAlignment == TitleAlignment.NearAxisArrow || axisY.TitleAlignment == TitleAlignment.NearAxisArrow))
            {
                Chart chart = (Chart)e.ChartElement;

                Graphics g = e.ChartGraphics.Graphics;

                Font drawFont = new Font("Verdana", 8);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                float x, y;
                // X AXIS
                if (axisX.TitleAlignment == TitleAlignment.NearAxisArrow)
                {
                    x = chart.Width - 90 - g.MeasureString(axisX.TextToDisplay, drawFont).Width;
                    y = (float)AxisY.ValueToPixelPosition(0) - 20;
                    g.DrawString(axisX.TextToDisplay, drawFont, drawBrush, x, y);
                }
                // Y AXIS
                if(axisY.TitleAlignment == TitleAlignment.NearAxisArrow)
                {
                    x = (float)AxisX.ValueToPixelPosition(0);
                    y = chart.Location.X;
                    g.DrawString(axisY.TextToDisplay, drawFont, drawBrush, x, y);
                }                    

                drawFont.Dispose();
                drawBrush.Dispose();
            }
        }

        // PROPERTIES
        /// <summary>
        /// Gets or sets the background color of FancyControls.Data.glGraph class.
        /// </summary>
        [System.ComponentModel.Browsable(true)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        [System.ComponentModel.Description("Gets or sets the background color of FancyControls.Data.glGraph class.")]
        public new Color BackColor
        {
            get
            {
                return graphArea.BackColor;
            }
            set
            {
                graphArea.Legends["graphLegend"].BackColor = value;
                graphArea.ChartAreas["CoordinateSystem"].BackColor = value;
                graphArea.BackColor = value;
                base.BackColor = value;
            }
        }
        /// <summary>
        /// Gets or sets the text of the legend title.
        /// </summary>
        [System.ComponentModel.Browsable(true)]
        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.DefaultValue("")]
        [System.ComponentModel.Description("Gets or sets the text of the legend title.")]
        public string LegendTitle
        {
            get
            {
                return graphArea.Legends["graphLegend"].Title;
            }
            set
            {
                graphArea.Legends["graphLegend"].Title = value;
            }
        }
        /// <summary>
        /// Gets an X axis.
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public Axis AxisX => axisX;
        /// <summary>
        /// Gets an Y axis.
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public Axis AxisY => axisY;

        // METHODS
        /// <summary>
        /// Shows array of graphs.
        /// </summary>
        /// <param name="graphs">
        /// The array of instance of FancyControls.Data.Graph class.
        /// </param>
        public void ShowGraphs(params Graph[] graphs)
        {
            foreach(Graph g in graphs)
            {
                this.graphArea.Series.Add(g.GetGraphConfig);
            }
        }
    }
}
