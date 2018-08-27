namespace SearchAlgorithms
{
    partial class MainForm
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
            algorithmThread?.Abort();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.scaleTbl = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.generatorLb = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.algorithmLb = new System.Windows.Forms.ListBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.delayUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.rowsUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.colsUpDown = new System.Windows.Forms.NumericUpDown();
            this.mazePnl = new System.Windows.Forms.Panel();
            this.scaleTbl.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.delayUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rowsUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colsUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // scaleTbl
            // 
            this.scaleTbl.ColumnCount = 1;
            this.scaleTbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.scaleTbl.Controls.Add(this.label1, 0, 0);
            this.scaleTbl.Controls.Add(this.tableLayoutPanel1, 0, 2);
            this.scaleTbl.Controls.Add(this.mazePnl, 0, 1);
            this.scaleTbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleTbl.Location = new System.Drawing.Point(0, 0);
            this.scaleTbl.Name = "scaleTbl";
            this.scaleTbl.RowCount = 3;
            this.scaleTbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.scaleTbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.scaleTbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.scaleTbl.Size = new System.Drawing.Size(539, 522);
            this.scaleTbl.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(533, 40);
            this.label1.TabIndex = 1;
            this.label1.Text = "Search Algorithms";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.generatorLb, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 380);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(533, 139);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // generatorLb
            // 
            this.generatorLb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.generatorLb.FormattingEnabled = true;
            this.generatorLb.Location = new System.Drawing.Point(269, 33);
            this.generatorLb.Name = "generatorLb";
            this.generatorLb.Size = new System.Drawing.Size(261, 103);
            this.generatorLb.TabIndex = 0;
            this.generatorLb.SelectedIndexChanged += new System.EventHandler(this.generatorLb_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(260, 30);
            this.label2.TabIndex = 0;
            this.label2.Text = "Algorithm";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(269, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(261, 30);
            this.label3.TabIndex = 1;
            this.label3.Text = "Maze Generator";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.Controls.Add(this.algorithmLb, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 30);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(266, 109);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // algorithmLb
            // 
            this.algorithmLb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.algorithmLb.FormattingEnabled = true;
            this.algorithmLb.Location = new System.Drawing.Point(3, 3);
            this.algorithmLb.Name = "algorithmLb";
            this.algorithmLb.Size = new System.Drawing.Size(153, 103);
            this.algorithmLb.TabIndex = 0;
            this.algorithmLb.SelectedIndexChanged += new System.EventHandler(this.algorithmLb_SelectedIndexChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label4);
            this.flowLayoutPanel1.Controls.Add(this.delayUpDown);
            this.flowLayoutPanel1.Controls.Add(this.label5);
            this.flowLayoutPanel1.Controls.Add(this.rowsUpDown);
            this.flowLayoutPanel1.Controls.Add(this.label6);
            this.flowLayoutPanel1.Controls.Add(this.colsUpDown);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(159, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(107, 109);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Delay";
            // 
            // delayUpDown
            // 
            this.flowLayoutPanel1.SetFlowBreak(this.delayUpDown, true);
            this.delayUpDown.Location = new System.Drawing.Point(49, 3);
            this.delayUpDown.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.delayUpDown.Name = "delayUpDown";
            this.delayUpDown.Size = new System.Drawing.Size(40, 20);
            this.delayUpDown.TabIndex = 1;
            this.delayUpDown.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.delayUpDown.ValueChanged += new System.EventHandler(this.delayUpDown_ValueChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 23);
            this.label5.TabIndex = 2;
            this.label5.Text = "Rows";
            // 
            // rowsUpDown
            // 
            this.flowLayoutPanel1.SetFlowBreak(this.rowsUpDown, true);
            this.rowsUpDown.Location = new System.Drawing.Point(49, 29);
            this.rowsUpDown.Maximum = new decimal(new int[] {
            230,
            0,
            0,
            0});
            this.rowsUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.rowsUpDown.Name = "rowsUpDown";
            this.rowsUpDown.Size = new System.Drawing.Size(40, 20);
            this.rowsUpDown.TabIndex = 3;
            this.rowsUpDown.Value = new decimal(new int[] {
            82,
            0,
            0,
            0});
            this.rowsUpDown.ValueChanged += new System.EventHandler(this.rowsUpDown_ValueChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 23);
            this.label6.TabIndex = 4;
            this.label6.Text = "Cols";
            // 
            // colsUpDown
            // 
            this.flowLayoutPanel1.SetFlowBreak(this.colsUpDown, true);
            this.colsUpDown.Location = new System.Drawing.Point(49, 55);
            this.colsUpDown.Maximum = new decimal(new int[] {
            270,
            0,
            0,
            0});
            this.colsUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.colsUpDown.Name = "colsUpDown";
            this.colsUpDown.Size = new System.Drawing.Size(40, 20);
            this.colsUpDown.TabIndex = 5;
            this.colsUpDown.Value = new decimal(new int[] {
            106,
            0,
            0,
            0});
            this.colsUpDown.ValueChanged += new System.EventHandler(this.colsUpDown_ValueChanged);
            // 
            // mazePnl
            // 
            this.mazePnl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mazePnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mazePnl.Location = new System.Drawing.Point(3, 43);
            this.mazePnl.Name = "mazePnl";
            this.mazePnl.Size = new System.Drawing.Size(533, 331);
            this.mazePnl.TabIndex = 3;
            this.mazePnl.SizeChanged += new System.EventHandler(this.mazePnl_SizeChanged);
            this.mazePnl.Paint += new System.Windows.Forms.PaintEventHandler(this.mazePnl_Paint);
            this.mazePnl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mazePnl_MouseDown);
            this.mazePnl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mazePnl_MouseMove);
            this.mazePnl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mazePnl_MouseUp);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 522);
            this.Controls.Add(this.scaleTbl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(480, 450);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search Algorithms";
            this.scaleTbl.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.delayUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rowsUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colsUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel scaleTbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel mazePnl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ListBox algorithmLb;
        private System.Windows.Forms.ListBox generatorLb;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown delayUpDown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown rowsUpDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown colsUpDown;
    }
}

