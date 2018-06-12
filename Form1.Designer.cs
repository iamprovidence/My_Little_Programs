namespace SnippetCreation
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
            this.headerGroupBox = new System.Windows.Forms.GroupBox();
            this.ShortcutLabel = new System.Windows.Forms.Label();
            this.DescLabel = new System.Windows.Forms.Label();
            this.AuthorLabel = new System.Windows.Forms.Label();
            this.ShortcutTextBox = new System.Windows.Forms.TextBox();
            this.DescTextBox = new System.Windows.Forms.TextBox();
            this.AuthorTextBox = new System.Windows.Forms.TextBox();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.TitleTextBox = new System.Windows.Forms.TextBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.create_btn = new System.Windows.Forms.Button();
            this.OptionGroupBox = new System.Windows.Forms.GroupBox();
            this.NamespaceTreeView = new System.Windows.Forms.TreeView();
            this.NamespaceLabel = new System.Windows.Forms.Label();
            this.CodeGroupBox = new System.Windows.Forms.GroupBox();
            this.remove_btn = new System.Windows.Forms.PictureBox();
            this.Add_btn = new System.Windows.Forms.PictureBox();
            this.ChangeELGroupBox = new System.Windows.Forms.GroupBox();
            this.DefaultLabel = new System.Windows.Forms.Label();
            this.ToolTipLabel = new System.Windows.Forms.Label();
            this.IDLabel = new System.Windows.Forms.Label();
            this.DefaultTextBox = new System.Windows.Forms.TextBox();
            this.ToolTipTextBox = new System.Windows.Forms.TextBox();
            this.IdTextBox = new System.Windows.Forms.TextBox();
            this.LitetalListBox = new System.Windows.Forms.ListBox();
            this.CodeRichTextBox = new System.Windows.Forms.RichTextBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.headerGroupBox.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.OptionGroupBox.SuspendLayout();
            this.CodeGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.remove_btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Add_btn)).BeginInit();
            this.ChangeELGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerGroupBox
            // 
            this.headerGroupBox.Controls.Add(this.ShortcutLabel);
            this.headerGroupBox.Controls.Add(this.DescLabel);
            this.headerGroupBox.Controls.Add(this.AuthorLabel);
            this.headerGroupBox.Controls.Add(this.ShortcutTextBox);
            this.headerGroupBox.Controls.Add(this.DescTextBox);
            this.headerGroupBox.Controls.Add(this.AuthorTextBox);
            this.headerGroupBox.Controls.Add(this.TitleLabel);
            this.headerGroupBox.Controls.Add(this.TitleTextBox);
            this.headerGroupBox.Location = new System.Drawing.Point(12, 27);
            this.headerGroupBox.Name = "headerGroupBox";
            this.headerGroupBox.Size = new System.Drawing.Size(221, 161);
            this.headerGroupBox.TabIndex = 0;
            this.headerGroupBox.TabStop = false;
            this.headerGroupBox.Text = "Header";
            // 
            // ShortcutLabel
            // 
            this.ShortcutLabel.AutoSize = true;
            this.ShortcutLabel.Location = new System.Drawing.Point(19, 119);
            this.ShortcutLabel.Name = "ShortcutLabel";
            this.ShortcutLabel.Size = new System.Drawing.Size(47, 13);
            this.ShortcutLabel.TabIndex = 6;
            this.ShortcutLabel.Text = "Shortcut";
            // 
            // DescLabel
            // 
            this.DescLabel.AutoSize = true;
            this.DescLabel.Location = new System.Drawing.Point(19, 93);
            this.DescLabel.Name = "DescLabel";
            this.DescLabel.Size = new System.Drawing.Size(60, 13);
            this.DescLabel.TabIndex = 5;
            this.DescLabel.Text = "Description";
            // 
            // AuthorLabel
            // 
            this.AuthorLabel.AutoSize = true;
            this.AuthorLabel.Location = new System.Drawing.Point(19, 67);
            this.AuthorLabel.Name = "AuthorLabel";
            this.AuthorLabel.Size = new System.Drawing.Size(38, 13);
            this.AuthorLabel.TabIndex = 4;
            this.AuthorLabel.Text = "Author";
            // 
            // ShortcutTextBox
            // 
            this.ShortcutTextBox.Location = new System.Drawing.Point(96, 119);
            this.ShortcutTextBox.Name = "ShortcutTextBox";
            this.ShortcutTextBox.Size = new System.Drawing.Size(119, 20);
            this.ShortcutTextBox.TabIndex = 2;
            // 
            // DescTextBox
            // 
            this.DescTextBox.Location = new System.Drawing.Point(96, 93);
            this.DescTextBox.Name = "DescTextBox";
            this.DescTextBox.Size = new System.Drawing.Size(119, 20);
            this.DescTextBox.TabIndex = 3;
            // 
            // AuthorTextBox
            // 
            this.AuthorTextBox.Location = new System.Drawing.Point(96, 67);
            this.AuthorTextBox.Name = "AuthorTextBox";
            this.AuthorTextBox.Size = new System.Drawing.Size(119, 20);
            this.AuthorTextBox.TabIndex = 2;
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Location = new System.Drawing.Point(19, 41);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(27, 13);
            this.TitleLabel.TabIndex = 1;
            this.TitleLabel.Text = "Title";
            // 
            // TitleTextBox
            // 
            this.TitleTextBox.Location = new System.Drawing.Point(96, 41);
            this.TitleTextBox.Name = "TitleTextBox";
            this.TitleTextBox.Size = new System.Drawing.Size(119, 20);
            this.TitleTextBox.TabIndex = 0;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(829, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.newToolStripMenuItem.Text = "&New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "&Open";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(143, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(143, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // create_btn
            // 
            this.create_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.create_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.create_btn.Location = new System.Drawing.Point(314, 318);
            this.create_btn.Name = "create_btn";
            this.create_btn.Size = new System.Drawing.Size(204, 37);
            this.create_btn.TabIndex = 2;
            this.create_btn.Text = "create";
            this.create_btn.UseVisualStyleBackColor = true;
            this.create_btn.Click += new System.EventHandler(this.create_btn_Click);
            // 
            // OptionGroupBox
            // 
            this.OptionGroupBox.Controls.Add(this.NamespaceTreeView);
            this.OptionGroupBox.Controls.Add(this.NamespaceLabel);
            this.OptionGroupBox.Location = new System.Drawing.Point(239, 28);
            this.OptionGroupBox.Name = "OptionGroupBox";
            this.OptionGroupBox.Size = new System.Drawing.Size(220, 270);
            this.OptionGroupBox.TabIndex = 3;
            this.OptionGroupBox.TabStop = false;
            this.OptionGroupBox.Text = "Options";
            // 
            // NamespaceTreeView
            // 
            this.NamespaceTreeView.CheckBoxes = true;
            this.NamespaceTreeView.FullRowSelect = true;
            this.NamespaceTreeView.Location = new System.Drawing.Point(6, 57);
            this.NamespaceTreeView.Name = "NamespaceTreeView";
            this.NamespaceTreeView.ShowLines = false;
            this.NamespaceTreeView.Size = new System.Drawing.Size(208, 201);
            this.NamespaceTreeView.TabIndex = 1;
            // 
            // NamespaceLabel
            // 
            this.NamespaceLabel.AutoSize = true;
            this.NamespaceLabel.Location = new System.Drawing.Point(21, 28);
            this.NamespaceLabel.Name = "NamespaceLabel";
            this.NamespaceLabel.Size = new System.Drawing.Size(113, 13);
            this.NamespaceLabel.TabIndex = 0;
            this.NamespaceLabel.Text = "DLL and Namespaces";
            // 
            // CodeGroupBox
            // 
            this.CodeGroupBox.Controls.Add(this.remove_btn);
            this.CodeGroupBox.Controls.Add(this.Add_btn);
            this.CodeGroupBox.Controls.Add(this.ChangeELGroupBox);
            this.CodeGroupBox.Controls.Add(this.CodeRichTextBox);
            this.CodeGroupBox.Location = new System.Drawing.Point(467, 28);
            this.CodeGroupBox.Name = "CodeGroupBox";
            this.CodeGroupBox.Size = new System.Drawing.Size(349, 270);
            this.CodeGroupBox.TabIndex = 4;
            this.CodeGroupBox.TabStop = false;
            this.CodeGroupBox.Text = "Code";
            // 
            // remove_btn
            // 
            this.remove_btn.BackgroundImage = global::SnippetCreation.Properties.Resources.remove;
            this.remove_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.remove_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.remove_btn.Location = new System.Drawing.Point(318, 162);
            this.remove_btn.Name = "remove_btn";
            this.remove_btn.Size = new System.Drawing.Size(20, 20);
            this.remove_btn.TabIndex = 3;
            this.remove_btn.TabStop = false;
            // 
            // Add_btn
            // 
            this.Add_btn.BackgroundImage = global::SnippetCreation.Properties.Resources.red_plus_512;
            this.Add_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Add_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Add_btn.Location = new System.Drawing.Point(318, 135);
            this.Add_btn.Name = "Add_btn";
            this.Add_btn.Size = new System.Drawing.Size(20, 20);
            this.Add_btn.TabIndex = 2;
            this.Add_btn.TabStop = false;
            this.Add_btn.Click += new System.EventHandler(this.Add_btn_Click);
            // 
            // ChangeELGroupBox
            // 
            this.ChangeELGroupBox.Controls.Add(this.DefaultLabel);
            this.ChangeELGroupBox.Controls.Add(this.ToolTipLabel);
            this.ChangeELGroupBox.Controls.Add(this.IDLabel);
            this.ChangeELGroupBox.Controls.Add(this.DefaultTextBox);
            this.ChangeELGroupBox.Controls.Add(this.ToolTipTextBox);
            this.ChangeELGroupBox.Controls.Add(this.IdTextBox);
            this.ChangeELGroupBox.Controls.Add(this.LitetalListBox);
            this.ChangeELGroupBox.Location = new System.Drawing.Point(15, 20);
            this.ChangeELGroupBox.Name = "ChangeELGroupBox";
            this.ChangeELGroupBox.Size = new System.Drawing.Size(328, 100);
            this.ChangeELGroupBox.TabIndex = 1;
            this.ChangeELGroupBox.TabStop = false;
            this.ChangeELGroupBox.Text = "Litelar";
            // 
            // DefaultLabel
            // 
            this.DefaultLabel.AutoSize = true;
            this.DefaultLabel.Location = new System.Drawing.Point(143, 67);
            this.DefaultLabel.Name = "DefaultLabel";
            this.DefaultLabel.Size = new System.Drawing.Size(41, 13);
            this.DefaultLabel.TabIndex = 6;
            this.DefaultLabel.Text = "Default";
            // 
            // ToolTipLabel
            // 
            this.ToolTipLabel.AutoSize = true;
            this.ToolTipLabel.Location = new System.Drawing.Point(143, 41);
            this.ToolTipLabel.Name = "ToolTipLabel";
            this.ToolTipLabel.Size = new System.Drawing.Size(43, 13);
            this.ToolTipLabel.TabIndex = 5;
            this.ToolTipLabel.Text = "ToolTip";
            // 
            // IDLabel
            // 
            this.IDLabel.AutoSize = true;
            this.IDLabel.Location = new System.Drawing.Point(143, 14);
            this.IDLabel.Name = "IDLabel";
            this.IDLabel.Size = new System.Drawing.Size(18, 13);
            this.IDLabel.TabIndex = 4;
            this.IDLabel.Text = "ID";
            // 
            // DefaultTextBox
            // 
            this.DefaultTextBox.Location = new System.Drawing.Point(205, 67);
            this.DefaultTextBox.Name = "DefaultTextBox";
            this.DefaultTextBox.Size = new System.Drawing.Size(100, 20);
            this.DefaultTextBox.TabIndex = 3;
            this.DefaultTextBox.TextChanged += new System.EventHandler(this.DefaultTextBox_TextChanged);
            // 
            // ToolTipTextBox
            // 
            this.ToolTipTextBox.Location = new System.Drawing.Point(205, 40);
            this.ToolTipTextBox.Name = "ToolTipTextBox";
            this.ToolTipTextBox.Size = new System.Drawing.Size(100, 20);
            this.ToolTipTextBox.TabIndex = 2;
            this.ToolTipTextBox.TextChanged += new System.EventHandler(this.ToolTipTextBox_TextChanged);
            // 
            // IdTextBox
            // 
            this.IdTextBox.Location = new System.Drawing.Point(205, 14);
            this.IdTextBox.Name = "IdTextBox";
            this.IdTextBox.Size = new System.Drawing.Size(100, 20);
            this.IdTextBox.TabIndex = 1;
            this.IdTextBox.TextChanged += new System.EventHandler(this.IdTextBox_TextChanged);
            // 
            // LitetalListBox
            // 
            this.LitetalListBox.FormattingEnabled = true;
            this.LitetalListBox.Location = new System.Drawing.Point(13, 19);
            this.LitetalListBox.Name = "LitetalListBox";
            this.LitetalListBox.Size = new System.Drawing.Size(120, 69);
            this.LitetalListBox.TabIndex = 0;
            this.LitetalListBox.SelectedIndexChanged += new System.EventHandler(this.LitetalListBox_SelectedIndexChanged);
            // 
            // CodeRichTextBox
            // 
            this.CodeRichTextBox.Location = new System.Drawing.Point(15, 133);
            this.CodeRichTextBox.Name = "CodeRichTextBox";
            this.CodeRichTextBox.Size = new System.Drawing.Size(297, 125);
            this.CodeRichTextBox.TabIndex = 0;
            this.CodeRichTextBox.Text = "";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Snippet | *.snippet";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 367);
            this.Controls.Add(this.CodeGroupBox);
            this.Controls.Add(this.OptionGroupBox);
            this.Controls.Add(this.create_btn);
            this.Controls.Add(this.headerGroupBox);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(845, 405);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(845, 405);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Snippet Builder";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.headerGroupBox.ResumeLayout(false);
            this.headerGroupBox.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.OptionGroupBox.ResumeLayout(false);
            this.OptionGroupBox.PerformLayout();
            this.CodeGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.remove_btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Add_btn)).EndInit();
            this.ChangeELGroupBox.ResumeLayout(false);
            this.ChangeELGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox headerGroupBox;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label ShortcutLabel;
        private System.Windows.Forms.Label DescLabel;
        private System.Windows.Forms.Label AuthorLabel;
        private System.Windows.Forms.TextBox ShortcutTextBox;
        private System.Windows.Forms.TextBox DescTextBox;
        private System.Windows.Forms.TextBox AuthorTextBox;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.TextBox TitleTextBox;
        private System.Windows.Forms.Button create_btn;
        private System.Windows.Forms.GroupBox OptionGroupBox;
        private System.Windows.Forms.Label NamespaceLabel;
        private System.Windows.Forms.GroupBox CodeGroupBox;
        private System.Windows.Forms.GroupBox ChangeELGroupBox;
        private System.Windows.Forms.Label DefaultLabel;
        private System.Windows.Forms.Label ToolTipLabel;
        private System.Windows.Forms.Label IDLabel;
        private System.Windows.Forms.TextBox DefaultTextBox;
        private System.Windows.Forms.TextBox ToolTipTextBox;
        private System.Windows.Forms.TextBox IdTextBox;
        private System.Windows.Forms.ListBox LitetalListBox;
        private System.Windows.Forms.RichTextBox CodeRichTextBox;
        private System.Windows.Forms.PictureBox Add_btn;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.PictureBox remove_btn;
        private System.Windows.Forms.TreeView NamespaceTreeView;
    }
}

