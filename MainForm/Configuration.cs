using System;
using System.IO;
using System.Drawing;

namespace SnippetCreation
{
    public partial class MainForm
    {
        // FIELDS
        string AssembliesFileRoot;
        string NameSpacesFileRoot;
        bool runFinding;

        System.Xml.XmlWriter xmlWriter;
        System.Xml.XmlReader xmlReader;

        ListLiteral LiteralList;

        Color[] ArrColor;
        int colorIndex;

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
    }
}
