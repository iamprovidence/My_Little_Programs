namespace AdvancedDictionary.UserControls
{
    partial class Pourer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pourer));
            this.toRightBtn = new System.Windows.Forms.Button();
            this.toLeftBtn = new System.Windows.Forms.Button();
            this.leftLbx = new System.Windows.Forms.ListBox();
            this.rightLbx = new System.Windows.Forms.ListBox();
            this.TextLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // toRightBtn
            // 
            this.toRightBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.toRightBtn.BackColor = System.Drawing.Color.Transparent;
            this.toRightBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toRightBtn.BackgroundImage")));
            this.toRightBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toRightBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.toRightBtn.FlatAppearance.BorderSize = 0;
            this.toRightBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.toRightBtn.Location = new System.Drawing.Point(119, 34);
            this.toRightBtn.Margin = new System.Windows.Forms.Padding(0);
            this.toRightBtn.Name = "toRightBtn";
            this.toRightBtn.Size = new System.Drawing.Size(53, 35);
            this.toRightBtn.TabIndex = 0;
            this.toRightBtn.TabStop = false;
            this.toRightBtn.UseVisualStyleBackColor = false;
            this.toRightBtn.Click += new System.EventHandler(this.toRightBtn_Click);
            this.toRightBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toRightMove_MousePressed);
            this.toRightBtn.MouseEnter += new System.EventHandler(this.toRightMove_MouseHover);
            this.toRightBtn.MouseLeave += new System.EventHandler(this.toLeftMove_MouseHover);
            this.toRightBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.toLeftMove_MousePressed);
            // 
            // toLeftBtn
            // 
            this.toLeftBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.toLeftBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toLeftBtn.BackgroundImage")));
            this.toLeftBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toLeftBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.toLeftBtn.FlatAppearance.BorderSize = 0;
            this.toLeftBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.toLeftBtn.Location = new System.Drawing.Point(90, 69);
            this.toLeftBtn.Margin = new System.Windows.Forms.Padding(0);
            this.toLeftBtn.Name = "toLeftBtn";
            this.toLeftBtn.Size = new System.Drawing.Size(55, 35);
            this.toLeftBtn.TabIndex = 1;
            this.toLeftBtn.TabStop = false;
            this.toLeftBtn.Click += new System.EventHandler(this.toLeftBtn_Click);
            this.toLeftBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toLeftMove_MousePressed);
            this.toLeftBtn.MouseEnter += new System.EventHandler(this.toLeftMove_MouseHover);
            this.toLeftBtn.MouseLeave += new System.EventHandler(this.toRightMove_MouseHover);
            this.toLeftBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.toRightMove_MousePressed);
            // 
            // leftLbx
            // 
            this.leftLbx.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftLbx.FormattingEnabled = true;
            this.leftLbx.Location = new System.Drawing.Point(0, 0);
            this.leftLbx.Margin = new System.Windows.Forms.Padding(0);
            this.leftLbx.Name = "leftLbx";
            this.leftLbx.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.leftLbx.Size = new System.Drawing.Size(87, 122);
            this.leftLbx.TabIndex = 2;
            // 
            // rightLbx
            // 
            this.rightLbx.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightLbx.FormattingEnabled = true;
            this.rightLbx.Location = new System.Drawing.Point(178, 0);
            this.rightLbx.Margin = new System.Windows.Forms.Padding(0);
            this.rightLbx.Name = "rightLbx";
            this.rightLbx.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.rightLbx.Size = new System.Drawing.Size(87, 122);
            this.rightLbx.TabIndex = 3;
            // 
            // TextLbl
            // 
            this.TextLbl.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TextLbl.Location = new System.Drawing.Point(87, 0);
            this.TextLbl.Margin = new System.Windows.Forms.Padding(0);
            this.TextLbl.MinimumSize = new System.Drawing.Size(0, 12);
            this.TextLbl.Name = "TextLbl";
            this.TextLbl.Size = new System.Drawing.Size(90, 12);
            this.TextLbl.TabIndex = 4;
            this.TextLbl.Text = "text";
            this.TextLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Pourer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.TextLbl);
            this.Controls.Add(this.toRightBtn);
            this.Controls.Add(this.leftLbx);
            this.Controls.Add(this.rightLbx);
            this.Controls.Add(this.toLeftBtn);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Pourer";
            this.Size = new System.Drawing.Size(265, 122);
            this.Resize += new System.EventHandler(this.Pourer2_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox leftLbx;
        private System.Windows.Forms.ListBox rightLbx;
        private System.Windows.Forms.Label TextLbl;
        private System.Windows.Forms.Button toLeftBtn;
        private System.Windows.Forms.Button toRightBtn;
    }
}
