namespace SnippetBuilder
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
            this.components = new System.ComponentModel.Container();
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
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.create_btn = new System.Windows.Forms.Button();
            this.OptionGroupBox = new System.Windows.Forms.GroupBox();
            this.DllAndNamespaceTreeView = new System.Windows.Forms.TreeView();
            this.NamespaceLabel = new System.Windows.Forms.Label();
            this.CodeGroupBox = new System.Windows.Forms.GroupBox();
            this.Add_btn = new System.Windows.Forms.PictureBox();
            this.CodeRichTextBox = new System.Windows.Forms.RichTextBox();
            this.ScaleTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.InnerTableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ChangeELGroupBox = new System.Windows.Forms.GroupBox();
            this.literalColorPanel = new System.Windows.Forms.Panel();
            this.remove_btn = new System.Windows.Forms.PictureBox();
            this.LiteralListBox = new System.Windows.Forms.ListBox();
            this.DefaultLabel = new System.Windows.Forms.Label();
            this.ToolTipLabel = new System.Windows.Forms.Label();
            this.IDLabel = new System.Windows.Forms.Label();
            this.DefaultTextBox = new System.Windows.Forms.TextBox();
            this.ToolTipTextBox = new System.Windows.Forms.TextBox();
            this.IdTextBox = new System.Windows.Forms.TextBox();
            this.InnerTableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.InnerTableLayoutPanel21 = new System.Windows.Forms.TableLayoutPanel();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.headerGroupBox.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.OptionGroupBox.SuspendLayout();
            this.CodeGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Add_btn)).BeginInit();
            this.ScaleTableLayoutPanel.SuspendLayout();
            this.InnerTableLayoutPanel1.SuspendLayout();
            this.ChangeELGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.remove_btn)).BeginInit();
            this.InnerTableLayoutPanel2.SuspendLayout();
            this.InnerTableLayoutPanel21.SuspendLayout();
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
            this.headerGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerGroupBox.Location = new System.Drawing.Point(3, 3);
            this.headerGroupBox.Name = "headerGroupBox";
            this.headerGroupBox.Size = new System.Drawing.Size(236, 159);
            this.headerGroupBox.TabIndex = 0;
            this.headerGroupBox.TabStop = false;
            this.headerGroupBox.Text = "Header";
            // 
            // ShortcutLabel
            // 
            this.ShortcutLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ShortcutLabel.AutoSize = true;
            this.ShortcutLabel.Location = new System.Drawing.Point(19, 116);
            this.ShortcutLabel.Name = "ShortcutLabel";
            this.ShortcutLabel.Size = new System.Drawing.Size(47, 13);
            this.ShortcutLabel.TabIndex = 6;
            this.ShortcutLabel.Text = "Shortcut";
            // 
            // DescLabel
            // 
            this.DescLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DescLabel.AutoSize = true;
            this.DescLabel.Location = new System.Drawing.Point(19, 90);
            this.DescLabel.Name = "DescLabel";
            this.DescLabel.Size = new System.Drawing.Size(60, 13);
            this.DescLabel.TabIndex = 5;
            this.DescLabel.Text = "Description";
            // 
            // AuthorLabel
            // 
            this.AuthorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AuthorLabel.AutoSize = true;
            this.AuthorLabel.Location = new System.Drawing.Point(19, 64);
            this.AuthorLabel.Name = "AuthorLabel";
            this.AuthorLabel.Size = new System.Drawing.Size(38, 13);
            this.AuthorLabel.TabIndex = 4;
            this.AuthorLabel.Text = "Author";
            // 
            // ShortcutTextBox
            // 
            this.ShortcutTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ShortcutTextBox.Location = new System.Drawing.Point(96, 116);
            this.ShortcutTextBox.Name = "ShortcutTextBox";
            this.ShortcutTextBox.Size = new System.Drawing.Size(108, 20);
            this.ShortcutTextBox.TabIndex = 2;
            // 
            // DescTextBox
            // 
            this.DescTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DescTextBox.Location = new System.Drawing.Point(96, 90);
            this.DescTextBox.Name = "DescTextBox";
            this.DescTextBox.Size = new System.Drawing.Size(108, 20);
            this.DescTextBox.TabIndex = 3;
            // 
            // AuthorTextBox
            // 
            this.AuthorTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AuthorTextBox.Location = new System.Drawing.Point(96, 64);
            this.AuthorTextBox.Name = "AuthorTextBox";
            this.AuthorTextBox.Size = new System.Drawing.Size(108, 20);
            this.AuthorTextBox.TabIndex = 2;
            // 
            // TitleLabel
            // 
            this.TitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Location = new System.Drawing.Point(19, 38);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(27, 13);
            this.TitleLabel.TabIndex = 1;
            this.TitleLabel.Text = "Title";
            // 
            // TitleTextBox
            // 
            this.TitleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TitleTextBox.Location = new System.Drawing.Point(96, 38);
            this.TitleTextBox.Name = "TitleTextBox";
            this.TitleTextBox.Size = new System.Drawing.Size(108, 20);
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
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // create_btn
            // 
            this.create_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.create_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.create_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.create_btn.Location = new System.Drawing.Point(368, 297);
            this.create_btn.Name = "create_btn";
            this.create_btn.Size = new System.Drawing.Size(204, 37);
            this.create_btn.TabIndex = 2;
            this.create_btn.Text = "create";
            this.create_btn.UseVisualStyleBackColor = true;
            this.create_btn.Click += new System.EventHandler(this.create_btn_Click);
            // 
            // OptionGroupBox
            // 
            this.OptionGroupBox.Controls.Add(this.DllAndNamespaceTreeView);
            this.OptionGroupBox.Controls.Add(this.NamespaceLabel);
            this.OptionGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OptionGroupBox.Location = new System.Drawing.Point(3, 3);
            this.OptionGroupBox.Name = "OptionGroupBox";
            this.OptionGroupBox.Size = new System.Drawing.Size(204, 281);
            this.OptionGroupBox.TabIndex = 3;
            this.OptionGroupBox.TabStop = false;
            this.OptionGroupBox.Text = "Options";
            // 
            // DllAndNamespaceTreeView
            // 
            this.DllAndNamespaceTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DllAndNamespaceTreeView.CheckBoxes = true;
            this.DllAndNamespaceTreeView.FullRowSelect = true;
            this.DllAndNamespaceTreeView.Location = new System.Drawing.Point(6, 44);
            this.DllAndNamespaceTreeView.Name = "DllAndNamespaceTreeView";
            this.DllAndNamespaceTreeView.ShowLines = false;
            this.DllAndNamespaceTreeView.Size = new System.Drawing.Size(192, 231);
            this.DllAndNamespaceTreeView.TabIndex = 1;
            this.DllAndNamespaceTreeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterCheck);
            // 
            // NamespaceLabel
            // 
            this.NamespaceLabel.AutoSize = true;
            this.NamespaceLabel.Location = new System.Drawing.Point(19, 18);
            this.NamespaceLabel.Name = "NamespaceLabel";
            this.NamespaceLabel.Size = new System.Drawing.Size(113, 13);
            this.NamespaceLabel.TabIndex = 0;
            this.NamespaceLabel.Text = "DLL and Namespaces";
            // 
            // CodeGroupBox
            // 
            this.CodeGroupBox.Controls.Add(this.Add_btn);
            this.CodeGroupBox.Controls.Add(this.CodeRichTextBox);
            this.CodeGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CodeGroupBox.Location = new System.Drawing.Point(213, 3);
            this.CodeGroupBox.Name = "CodeGroupBox";
            this.CodeGroupBox.Size = new System.Drawing.Size(353, 281);
            this.CodeGroupBox.TabIndex = 4;
            this.CodeGroupBox.TabStop = false;
            this.CodeGroupBox.Text = "Code";
            // 
            // Add_btn
            // 
            this.Add_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Add_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Add_btn.Image = global::SnippetCreation.Properties.Resources.red_plus_5121;
            this.Add_btn.Location = new System.Drawing.Point(6, 18);
            this.Add_btn.Name = "Add_btn";
            this.Add_btn.Size = new System.Drawing.Size(20, 20);
            this.Add_btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Add_btn.TabIndex = 2;
            this.Add_btn.TabStop = false;
            this.toolTip.SetToolTip(this.Add_btn, "Add selected text to literal");
            this.Add_btn.Click += new System.EventHandler(this.Add_btn_Click);
            // 
            // CodeRichTextBox
            // 
            this.CodeRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CodeRichTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.CodeRichTextBox.Location = new System.Drawing.Point(6, 44);
            this.CodeRichTextBox.Name = "CodeRichTextBox";
            this.CodeRichTextBox.Size = new System.Drawing.Size(341, 231);
            this.CodeRichTextBox.TabIndex = 0;
            this.CodeRichTextBox.Text = "";
            // 
            // ScaleTableLayoutPanel
            // 
            this.ScaleTableLayoutPanel.ColumnCount = 2;
            this.ScaleTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.ScaleTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.ScaleTableLayoutPanel.Controls.Add(this.InnerTableLayoutPanel1, 0, 0);
            this.ScaleTableLayoutPanel.Controls.Add(this.InnerTableLayoutPanel2, 1, 0);
            this.ScaleTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScaleTableLayoutPanel.Location = new System.Drawing.Point(0, 24);
            this.ScaleTableLayoutPanel.Name = "ScaleTableLayoutPanel";
            this.ScaleTableLayoutPanel.RowCount = 1;
            this.ScaleTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ScaleTableLayoutPanel.Size = new System.Drawing.Size(829, 343);
            this.ScaleTableLayoutPanel.TabIndex = 5;
            // 
            // InnerTableLayoutPanel1
            // 
            this.InnerTableLayoutPanel1.ColumnCount = 1;
            this.InnerTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.InnerTableLayoutPanel1.Controls.Add(this.headerGroupBox, 0, 0);
            this.InnerTableLayoutPanel1.Controls.Add(this.ChangeELGroupBox, 0, 1);
            this.InnerTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InnerTableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.InnerTableLayoutPanel1.Name = "InnerTableLayoutPanel1";
            this.InnerTableLayoutPanel1.RowCount = 2;
            this.InnerTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 165F));
            this.InnerTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.InnerTableLayoutPanel1.Size = new System.Drawing.Size(242, 337);
            this.InnerTableLayoutPanel1.TabIndex = 0;
            // 
            // ChangeELGroupBox
            // 
            this.ChangeELGroupBox.Controls.Add(this.literalColorPanel);
            this.ChangeELGroupBox.Controls.Add(this.remove_btn);
            this.ChangeELGroupBox.Controls.Add(this.LiteralListBox);
            this.ChangeELGroupBox.Controls.Add(this.DefaultLabel);
            this.ChangeELGroupBox.Controls.Add(this.ToolTipLabel);
            this.ChangeELGroupBox.Controls.Add(this.IDLabel);
            this.ChangeELGroupBox.Controls.Add(this.DefaultTextBox);
            this.ChangeELGroupBox.Controls.Add(this.ToolTipTextBox);
            this.ChangeELGroupBox.Controls.Add(this.IdTextBox);
            this.ChangeELGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChangeELGroupBox.Location = new System.Drawing.Point(3, 168);
            this.ChangeELGroupBox.Name = "ChangeELGroupBox";
            this.ChangeELGroupBox.Size = new System.Drawing.Size(236, 166);
            this.ChangeELGroupBox.TabIndex = 1;
            this.ChangeELGroupBox.TabStop = false;
            this.ChangeELGroupBox.Text = "Litelar";
            // 
            // literalColorPanel
            // 
            this.literalColorPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.literalColorPanel.BackColor = System.Drawing.Color.Yellow;
            this.literalColorPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.literalColorPanel.Location = new System.Drawing.Point(210, 49);
            this.literalColorPanel.Name = "literalColorPanel";
            this.literalColorPanel.Size = new System.Drawing.Size(20, 20);
            this.literalColorPanel.TabIndex = 7;
            this.toolTip.SetToolTip(this.literalColorPanel, "Set color to literal");
            this.literalColorPanel.Click += new System.EventHandler(this.literalColorPanel_Click);
            // 
            // remove_btn
            // 
            this.remove_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.remove_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.remove_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.remove_btn.Image = global::SnippetCreation.Properties.Resources.remove1;
            this.remove_btn.Location = new System.Drawing.Point(210, 23);
            this.remove_btn.Name = "remove_btn";
            this.remove_btn.Size = new System.Drawing.Size(20, 20);
            this.remove_btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.remove_btn.TabIndex = 3;
            this.remove_btn.TabStop = false;
            this.toolTip.SetToolTip(this.remove_btn, "Remove literal");
            this.remove_btn.Click += new System.EventHandler(this.Remove_btn_Click);
            // 
            // LiteralListBox
            // 
            this.LiteralListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LiteralListBox.FormattingEnabled = true;
            this.LiteralListBox.Location = new System.Drawing.Point(6, 108);
            this.LiteralListBox.Name = "LiteralListBox";
            this.LiteralListBox.Size = new System.Drawing.Size(224, 56);
            this.LiteralListBox.TabIndex = 0;
            this.LiteralListBox.SelectedIndexChanged += new System.EventHandler(this.LitetalListBox_SelectedIndexChanged);
            // 
            // DefaultLabel
            // 
            this.DefaultLabel.AutoSize = true;
            this.DefaultLabel.Location = new System.Drawing.Point(19, 79);
            this.DefaultLabel.Name = "DefaultLabel";
            this.DefaultLabel.Size = new System.Drawing.Size(41, 13);
            this.DefaultLabel.TabIndex = 6;
            this.DefaultLabel.Text = "Default";
            // 
            // ToolTipLabel
            // 
            this.ToolTipLabel.AutoSize = true;
            this.ToolTipLabel.Location = new System.Drawing.Point(19, 53);
            this.ToolTipLabel.Name = "ToolTipLabel";
            this.ToolTipLabel.Size = new System.Drawing.Size(43, 13);
            this.ToolTipLabel.TabIndex = 5;
            this.ToolTipLabel.Text = "ToolTip";
            // 
            // IDLabel
            // 
            this.IDLabel.AutoSize = true;
            this.IDLabel.Location = new System.Drawing.Point(19, 26);
            this.IDLabel.Name = "IDLabel";
            this.IDLabel.Size = new System.Drawing.Size(18, 13);
            this.IDLabel.TabIndex = 4;
            this.IDLabel.Text = "ID";
            // 
            // DefaultTextBox
            // 
            this.DefaultTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DefaultTextBox.Location = new System.Drawing.Point(96, 76);
            this.DefaultTextBox.Name = "DefaultTextBox";
            this.DefaultTextBox.Size = new System.Drawing.Size(108, 20);
            this.DefaultTextBox.TabIndex = 3;
            this.DefaultTextBox.TextChanged += new System.EventHandler(this.DefaultTextBox_TextChanged);
            // 
            // ToolTipTextBox
            // 
            this.ToolTipTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ToolTipTextBox.Location = new System.Drawing.Point(96, 49);
            this.ToolTipTextBox.Name = "ToolTipTextBox";
            this.ToolTipTextBox.Size = new System.Drawing.Size(108, 20);
            this.ToolTipTextBox.TabIndex = 2;
            this.ToolTipTextBox.TextChanged += new System.EventHandler(this.ToolTipTextBox_TextChanged);
            // 
            // IdTextBox
            // 
            this.IdTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IdTextBox.Location = new System.Drawing.Point(96, 23);
            this.IdTextBox.Name = "IdTextBox";
            this.IdTextBox.Size = new System.Drawing.Size(108, 20);
            this.IdTextBox.TabIndex = 1;
            this.IdTextBox.TextChanged += new System.EventHandler(this.IdTextBox_TextChanged);
            // 
            // InnerTableLayoutPanel2
            // 
            this.InnerTableLayoutPanel2.ColumnCount = 1;
            this.InnerTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.InnerTableLayoutPanel2.Controls.Add(this.InnerTableLayoutPanel21, 0, 0);
            this.InnerTableLayoutPanel2.Controls.Add(this.create_btn, 0, 1);
            this.InnerTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InnerTableLayoutPanel2.Location = new System.Drawing.Point(251, 3);
            this.InnerTableLayoutPanel2.Name = "InnerTableLayoutPanel2";
            this.InnerTableLayoutPanel2.RowCount = 2;
            this.InnerTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87F));
            this.InnerTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.InnerTableLayoutPanel2.Size = new System.Drawing.Size(575, 337);
            this.InnerTableLayoutPanel2.TabIndex = 1;
            // 
            // InnerTableLayoutPanel21
            // 
            this.InnerTableLayoutPanel21.ColumnCount = 2;
            this.InnerTableLayoutPanel21.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.0826F));
            this.InnerTableLayoutPanel21.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.9174F));
            this.InnerTableLayoutPanel21.Controls.Add(this.OptionGroupBox, 0, 0);
            this.InnerTableLayoutPanel21.Controls.Add(this.CodeGroupBox, 1, 0);
            this.InnerTableLayoutPanel21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InnerTableLayoutPanel21.Location = new System.Drawing.Point(3, 3);
            this.InnerTableLayoutPanel21.Name = "InnerTableLayoutPanel21";
            this.InnerTableLayoutPanel21.RowCount = 1;
            this.InnerTableLayoutPanel21.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.InnerTableLayoutPanel21.Size = new System.Drawing.Size(569, 287);
            this.InnerTableLayoutPanel21.TabIndex = 0;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Snippet (*.snippet) | *.snippet";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "Snippet (*.snippet)| *.snippet";
            // 
            // colorDialog
            // 
            this.colorDialog.Color = System.Drawing.Color.Red;
            this.colorDialog.SolidColorOnly = true;
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 1000;
            this.toolTip.IsBalloon = true;
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 367);
            this.Controls.Add(this.ScaleTableLayoutPanel);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(845, 405);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Snippet Builder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.headerGroupBox.ResumeLayout(false);
            this.headerGroupBox.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.OptionGroupBox.ResumeLayout(false);
            this.OptionGroupBox.PerformLayout();
            this.CodeGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Add_btn)).EndInit();
            this.ScaleTableLayoutPanel.ResumeLayout(false);
            this.InnerTableLayoutPanel1.ResumeLayout(false);
            this.ChangeELGroupBox.ResumeLayout(false);
            this.ChangeELGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.remove_btn)).EndInit();
            this.InnerTableLayoutPanel2.ResumeLayout(false);
            this.InnerTableLayoutPanel21.ResumeLayout(false);
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
        private System.Windows.Forms.ListBox LiteralListBox;
        private System.Windows.Forms.RichTextBox CodeRichTextBox;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.PictureBox remove_btn;
        private System.Windows.Forms.TreeView DllAndNamespaceTreeView;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TableLayoutPanel ScaleTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel InnerTableLayoutPanel1;
        private System.Windows.Forms.Panel literalColorPanel;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TableLayoutPanel InnerTableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel InnerTableLayoutPanel21;
        private System.Windows.Forms.PictureBox Add_btn;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    }
}
