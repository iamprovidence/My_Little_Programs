namespace FancyControls
{
    /// <summary>
    /// Represents a selector which contain unselected items in System.Windows.Forms.ComboBox.
    /// </summary>
    partial class SelectorBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectorBox));
            this.listBox = new System.Windows.Forms.ListBox();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.removeBtn = new System.Windows.Forms.PictureBox();
            this.addBtn = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.removeBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.addBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox
            // 
            this.listBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox.FormattingEnabled = true;
            this.listBox.Location = new System.Drawing.Point(0, 21);
            this.listBox.Name = "listBox";
            this.listBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox.Size = new System.Drawing.Size(79, 56);
            this.listBox.TabIndex = 0;
            // 
            // comboBox
            // 
            this.comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(0, 0);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(79, 21);
            this.comboBox.TabIndex = 1;
            // 
            // removeBtn
            // 
            this.removeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.removeBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.removeBtn.Image = ((System.Drawing.Image)(resources.GetObject("removeBtn.Image")));
            this.removeBtn.Location = new System.Drawing.Point(81, 41);
            this.removeBtn.Margin = new System.Windows.Forms.Padding(0);
            this.removeBtn.Name = "removeBtn";
            this.removeBtn.Size = new System.Drawing.Size(30, 28);
            this.removeBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.removeBtn.TabIndex = 3;
            this.removeBtn.TabStop = false;
            this.removeBtn.Click += new System.EventHandler(this.removeBtn_Click);
            this.removeBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_MouseDown);
            this.removeBtn.MouseEnter += new System.EventHandler(this.Btn_MouseEnter);
            this.removeBtn.MouseLeave += new System.EventHandler(this.Btn_MouseLeave);
            this.removeBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_MouseUp);
            // 
            // addBtn
            // 
            this.addBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.addBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addBtn.Image = ((System.Drawing.Image)(resources.GetObject("addBtn.Image")));
            this.addBtn.Location = new System.Drawing.Point(81, 8);
            this.addBtn.Margin = new System.Windows.Forms.Padding(0);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(30, 28);
            this.addBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.addBtn.TabIndex = 2;
            this.addBtn.TabStop = false;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            this.addBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_MouseDown);
            this.addBtn.MouseEnter += new System.EventHandler(this.Btn_MouseEnter);
            this.addBtn.MouseLeave += new System.EventHandler(this.Btn_MouseLeave);
            this.addBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_MouseUp);
            // 
            // SelectorBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.removeBtn);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.listBox);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SelectorBox";
            this.Size = new System.Drawing.Size(114, 78);
            ((System.ComponentModel.ISupportInitialize)(this.removeBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.addBtn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.PictureBox removeBtn;
        private System.Windows.Forms.PictureBox addBtn;
    }
}
