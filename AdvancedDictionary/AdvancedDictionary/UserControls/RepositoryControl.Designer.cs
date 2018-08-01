namespace AdvancedDictionary.UserControls
{
    partial class RepositoryControl
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
            this.itemsLb = new System.Windows.Forms.ListBox();
            this.addBtn = new System.Windows.Forms.Button();
            this.editBtn = new System.Windows.Forms.Button();
            this.addTb = new System.Windows.Forms.TextBox();
            this.editTb = new System.Windows.Forms.TextBox();
            this.removeBtn = new System.Windows.Forms.Button();
            this.mainTextLbl = new System.Windows.Forms.Label();
            this.scaleTlp = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.scaleTlp.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // itemsLb
            // 
            this.itemsLb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemsLb.FormattingEnabled = true;
            this.itemsLb.ItemHeight = 22;
            this.itemsLb.Location = new System.Drawing.Point(0, 0);
            this.itemsLb.Margin = new System.Windows.Forms.Padding(0);
            this.itemsLb.Name = "itemsLb";
            this.itemsLb.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.itemsLb.Size = new System.Drawing.Size(343, 251);
            this.itemsLb.TabIndex = 0;
            this.itemsLb.SelectedIndexChanged += new System.EventHandler(this.itemsLb_SelectedIndexChanged);
            // 
            // addBtn
            // 
            this.addBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addBtn.FlatAppearance.BorderSize = 0;
            this.addBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.addBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.addBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addBtn.Location = new System.Drawing.Point(0, 50);
            this.addBtn.Margin = new System.Windows.Forms.Padding(0);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(150, 50);
            this.addBtn.TabIndex = 1;
            this.addBtn.Text = "add";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // editBtn
            // 
            this.editBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.editBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editBtn.FlatAppearance.BorderSize = 0;
            this.editBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.editBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.editBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editBtn.Location = new System.Drawing.Point(0, 150);
            this.editBtn.Margin = new System.Windows.Forms.Padding(0);
            this.editBtn.Name = "editBtn";
            this.editBtn.Size = new System.Drawing.Size(150, 50);
            this.editBtn.TabIndex = 2;
            this.editBtn.Text = "edit";
            this.editBtn.UseVisualStyleBackColor = true;
            this.editBtn.Click += new System.EventHandler(this.editBtn_Click);
            // 
            // addTb
            // 
            this.addTb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addTb.Location = new System.Drawing.Point(0, 0);
            this.addTb.Margin = new System.Windows.Forms.Padding(0);
            this.addTb.Multiline = true;
            this.addTb.Name = "addTb";
            this.addTb.Size = new System.Drawing.Size(150, 50);
            this.addTb.TabIndex = 3;
            // 
            // editTb
            // 
            this.editTb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editTb.Location = new System.Drawing.Point(0, 100);
            this.editTb.Margin = new System.Windows.Forms.Padding(0);
            this.editTb.Multiline = true;
            this.editTb.Name = "editTb";
            this.editTb.Size = new System.Drawing.Size(150, 50);
            this.editTb.TabIndex = 4;
            // 
            // removeBtn
            // 
            this.removeBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.removeBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.removeBtn.FlatAppearance.BorderSize = 0;
            this.removeBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.removeBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.removeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeBtn.Location = new System.Drawing.Point(0, 200);
            this.removeBtn.Margin = new System.Windows.Forms.Padding(0);
            this.removeBtn.Name = "removeBtn";
            this.removeBtn.Size = new System.Drawing.Size(150, 51);
            this.removeBtn.TabIndex = 5;
            this.removeBtn.Text = "remove";
            this.removeBtn.UseVisualStyleBackColor = true;
            this.removeBtn.Click += new System.EventHandler(this.removeBtn_Click);
            // 
            // mainTextLbl
            // 
            this.mainTextLbl.AutoSize = true;
            this.mainTextLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTextLbl.Location = new System.Drawing.Point(0, 0);
            this.mainTextLbl.Margin = new System.Windows.Forms.Padding(0);
            this.mainTextLbl.Name = "mainTextLbl";
            this.mainTextLbl.Size = new System.Drawing.Size(493, 58);
            this.mainTextLbl.TabIndex = 6;
            this.mainTextLbl.Text = "repos name";
            this.mainTextLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scaleTlp
            // 
            this.scaleTlp.ColumnCount = 1;
            this.scaleTlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.scaleTlp.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.scaleTlp.Controls.Add(this.mainTextLbl, 0, 0);
            this.scaleTlp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleTlp.Location = new System.Drawing.Point(0, 0);
            this.scaleTlp.Margin = new System.Windows.Forms.Padding(0);
            this.scaleTlp.Name = "scaleTlp";
            this.scaleTlp.RowCount = 2;
            this.scaleTlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19F));
            this.scaleTlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81F));
            this.scaleTlp.Size = new System.Drawing.Size(493, 309);
            this.scaleTlp.TabIndex = 7;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Controls.Add(this.itemsLb, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 58);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(493, 251);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.addBtn, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.removeBtn, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.addTb, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.editBtn, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.editTb, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(343, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(150, 251);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // ReposControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scaleTlp);
            this.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "ReposControl";
            this.Size = new System.Drawing.Size(493, 309);
            this.scaleTlp.ResumeLayout(false);
            this.scaleTlp.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox itemsLb;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button editBtn;
        private System.Windows.Forms.TextBox addTb;
        private System.Windows.Forms.TextBox editTb;
        private System.Windows.Forms.Button removeBtn;
        private System.Windows.Forms.Label mainTextLbl;
        private System.Windows.Forms.TableLayoutPanel scaleTlp;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}
