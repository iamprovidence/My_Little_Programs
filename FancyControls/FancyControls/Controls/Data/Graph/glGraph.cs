using System.Drawing;
using System.Windows.Forms;

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
