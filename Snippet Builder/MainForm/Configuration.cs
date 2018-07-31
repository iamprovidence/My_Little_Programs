using System;
using System.Linq;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace SnippetBuilder
{
    public partial class MainForm
    {
        // FIELDS
        string AssembliesFileRoot;
        string NameSpacesFileRoot;
        string madeWith;
        bool runFinding;

        System.Xml.XmlWriter xmlWriter;

        ListLiteral LiteralList;

        Color[] ArrColor;
        int colorIndex;

        // PROPERTIES
        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        // CTORS
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            // fields
            AssembliesFileRoot = AppDomain.CurrentDomain.BaseDirectory + "Resources\\A.dat";
            NameSpacesFileRoot = AppDomain.CurrentDomain.BaseDirectory + "Resources\\N.dat";
            runFinding = true;
            madeWith = "Made with " + AssemblyProduct;

            LiteralList = new ListLiteral();

            ArrColor = new Color[] { Color.Red, Color.Orange, Color.Green, Color.Blue, Color.Violet };
            colorIndex = -1;

            // TreeView
            DllAndNamespaceTreeView.BeginUpdate();

            using (BinaryReader ReadAssembly = new BinaryReader(File.OpenRead(AssembliesFileRoot)),
                                ReadNamespace = new BinaryReader(File.OpenRead(NameSpacesFileRoot)))
            {
                int rootIndex = 0;
                int AssemblyCount = ReadAssembly.ReadInt32();

                while (AssemblyCount-- != 0)
                {
                    DllAndNamespaceTreeView.Nodes.Add(ReadAssembly.ReadString());
                    int NamespacesCount = ReadAssembly.ReadInt32();

                    while (NamespacesCount-- != 0)
                    {
                        DllAndNamespaceTreeView.Nodes[rootIndex].Nodes.Add(ReadNamespace.ReadString());
                    }

                    ++rootIndex;
                }
            }

            DllAndNamespaceTreeView.EndUpdate();
            //NamespaceTreeView.ExpandAll(); // розгортаємо все дерево
        }
        private void MainForm_Shown(object sender, EventArgs e)
        {
            ChangeTextColorInBackground();
        }
        private void MainForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            runFinding = false;
        }
        private void CleanUp()
        {
            TitleTextBox.Clear();
            AuthorTextBox.Clear();
            DescTextBox.Clear();
            ShortcutTextBox.Clear();

            IdTextBox.Clear();
            ToolTipTextBox.Clear();
            DefaultTextBox.Clear();

            CodeRichTextBox.Clear();
            LiteralListBox.Items.Clear();
            LiteralList.Clear();

            foreach(TreeNode tn in DllAndNamespaceTreeView.Nodes)
            {
                tn.Checked = false;
            }
        }
    }
}
