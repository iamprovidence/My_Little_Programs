namespace FancyControls
{
    /// <summary>
    /// Represents a colored counter.
    /// </summary>
    partial class Counter
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
            this.countLbl = new System.Windows.Forms.Label();
            this.textLbl = new System.Windows.Forms.Label();
            this.scaleTbl = new System.Windows.Forms.TableLayoutPanel();
            this.topFlp = new System.Windows.Forms.FlowLayoutPanel();
            this.maxLbl = new System.Windows.Forms.Label();
            this.separatorLbl = new System.Windows.Forms.Label();
            this.scaleTbl.SuspendLayout();
            this.topFlp.SuspendLayout();
            this.SuspendLayout();
            // 
            // countLbl
            // 
            this.countLbl.Cursor = System.Windows.Forms.Cursors.Default;
            this.countLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.countLbl.Font = new System.Drawing.Font("Verdana", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.countLbl.ForeColor = System.Drawing.Color.White;
            this.countLbl.Location = new System.Drawing.Point(0, 14);
            this.countLbl.Margin = new System.Windows.Forms.Padding(0);
            this.countLbl.Name = "countLbl";
            this.countLbl.Size = new System.Drawing.Size(145, 105);
            this.countLbl.TabIndex = 0;
            this.countLbl.Text = "0";
            this.countLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textLbl
            // 
            this.textLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textLbl.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textLbl.ForeColor = System.Drawing.Color.White;
            this.textLbl.Location = new System.Drawing.Point(0, 119);
            this.textLbl.Margin = new System.Windows.Forms.Padding(0);
            this.textLbl.Name = "textLbl";
            this.textLbl.Size = new System.Drawing.Size(145, 21);
            this.textLbl.TabIndex = 1;
            this.textLbl.Text = "Incomplete Tasks";
            // 
            // scaleTbl
            // 
            this.scaleTbl.ColumnCount = 1;
            this.scaleTbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.scaleTbl.Controls.Add(this.topFlp, 0, 0);
            this.scaleTbl.Controls.Add(this.textLbl, 0, 2);
            this.scaleTbl.Controls.Add(this.countLbl, 0, 1);
            this.scaleTbl.Cursor = System.Windows.Forms.Cursors.Default;
            this.scaleTbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleTbl.Location = new System.Drawing.Point(0, 0);
            this.scaleTbl.Margin = new System.Windows.Forms.Padding(6);
            this.scaleTbl.Name = "scaleTbl";
            this.scaleTbl.RowCount = 3;
            this.scaleTbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.scaleTbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.scaleTbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.scaleTbl.Size = new System.Drawing.Size(145, 140);
            this.scaleTbl.TabIndex = 2;
            // 
            // topFlp
            // 
            this.topFlp.Controls.Add(this.maxLbl);
            this.topFlp.Controls.Add(this.separatorLbl);
            this.topFlp.Cursor = System.Windows.Forms.Cursors.Default;
            this.topFlp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topFlp.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.topFlp.Location = new System.Drawing.Point(0, 0);
            this.topFlp.Margin = new System.Windows.Forms.Padding(0);
            this.topFlp.Name = "topFlp";
            this.topFlp.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.topFlp.Size = new System.Drawing.Size(145, 14);
            this.topFlp.TabIndex = 2;
            // 
            // maxLbl
            // 
            this.maxLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.maxLbl.AutoSize = true;
            this.maxLbl.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.maxLbl.ForeColor = System.Drawing.Color.White;
            this.maxLbl.Location = new System.Drawing.Point(124, 1);
            this.maxLbl.Margin = new System.Windows.Forms.Padding(0);
            this.maxLbl.Name = "maxLbl";
            this.maxLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.maxLbl.Size = new System.Drawing.Size(21, 13);
            this.maxLbl.TabIndex = 0;
            this.maxLbl.Text = "10";
            // 
            // separatorLbl
            // 
            this.separatorLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.separatorLbl.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.separatorLbl.ForeColor = System.Drawing.Color.White;
            this.separatorLbl.Location = new System.Drawing.Point(113, 0);
            this.separatorLbl.Margin = new System.Windows.Forms.Padding(0);
            this.separatorLbl.Name = "separatorLbl";
            this.separatorLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.separatorLbl.Size = new System.Drawing.Size(11, 14);
            this.separatorLbl.TabIndex = 1;
            this.separatorLbl.Text = "/";
            // 
            // Counter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.Controls.Add(this.scaleTbl);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Counter";
            this.Size = new System.Drawing.Size(145, 140);
            this.CursorChanged += new System.EventHandler(this.Counter_CursorChanged);
            this.Resize += new System.EventHandler(this.Counter_Resize);
            this.scaleTbl.ResumeLayout(false);
            this.topFlp.ResumeLayout(false);
            this.topFlp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label countLbl;
        private System.Windows.Forms.Label textLbl;
        private System.Windows.Forms.TableLayoutPanel scaleTbl;
        private System.Windows.Forms.FlowLayoutPanel topFlp;
        private System.Windows.Forms.Label maxLbl;
        private System.Windows.Forms.Label separatorLbl;
    }
}
