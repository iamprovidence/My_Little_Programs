namespace FancyControls.Data
{
    /// <summary>
    /// Represents a graph.
    /// </summary>
    partial class glGraph
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.graphArea = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.graphArea)).BeginInit();
            this.SuspendLayout();
            // 
            // graphArea
            // 
            this.graphArea.BorderlineWidth = 0;
            chartArea1.AxisX.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Lines;
            chartArea1.AxisX.MajorGrid.LineWidth = 0;
            chartArea1.AxisX.Title = "X Axis";
            chartArea1.AxisY.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Lines;
            chartArea1.AxisY.MajorGrid.LineWidth = 0;
            chartArea1.AxisY.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated270;
            chartArea1.AxisY.Title = "Y Axis";
            chartArea1.BackColor = System.Drawing.Color.White;
            chartArea1.BackSecondaryColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "CoordinateSystem";
            this.graphArea.ChartAreas.Add(chartArea1);
            this.graphArea.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "graphLegend";
            this.graphArea.Legends.Add(legend1);
            this.graphArea.Location = new System.Drawing.Point(0, 0);
            this.graphArea.Margin = new System.Windows.Forms.Padding(0);
            this.graphArea.Name = "graphArea";
            this.graphArea.Size = new System.Drawing.Size(459, 343);
            this.graphArea.TabIndex = 0;
            // 
            // GlGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.graphArea);
            this.Name = "GlGraph";
            this.Size = new System.Drawing.Size(459, 343);
            ((System.ComponentModel.ISupportInitialize)(this.graphArea)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart graphArea;
    }
}
