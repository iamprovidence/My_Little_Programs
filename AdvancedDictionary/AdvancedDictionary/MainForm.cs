using System;
using System.Linq;
using System.Windows.Forms;
using AdvancedDictionary.Model;
using AdvancedDictionary.Interfaces;
using static AdvancedDictionary.AdditionalClasses.Constants;

namespace AdvancedDictionary
{
    public partial class MainForm : Form
    {
        EmotionsRepository emotionsRepository;

        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            treeView.ExpandAll();
            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Files");
            EmotionsRepository.Create(out emotionsRepository, AppDomain.CurrentDomain.BaseDirectory + "Resources\\Files\\Emotions.dat");
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SaveOpenedPanals();
            panel.Controls.Clear();
            TreeNode node = e.Node;

            if(node.Parent != null)
            {
                string filePath = String.Concat(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Files\\", node.Text, ".dat");
                
                // Getting parent node name, so we know which control to create
                while (node.Parent != null) node = node.Parent;

                string[] topNodeTextArr = treeView.Nodes.OfType<TreeNode>().Select(x => x.Text).ToArray();

                
                if (node.Text == topNodeTextArr[0])
                {
                    new UserControls.RepositoryControl()
                    {
                        Parent = panel,
                        Dock = DockStyle.Fill
                    }.Build(filePath, emotionsRepository);
                }
                else if (node.Text == topNodeTextArr[1])
                {
                    new UserControls.WordsControl()
                    {
                        Parent = panel,
                        Dock = DockStyle.Fill
                    }.Build(filePath, emotionsRepository);
                }
            }
            
        }
        private void SaveOpenedPanals()
        {
            if (panel.Controls.Count != EMPTY_COLLECTION_SIZE)
            {
                foreach (ISaveable saveMe in panel.Controls.OfType<ISaveable>())
                {
                    saveMe.SaveToFile();
                }
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            SaveOpenedPanals();
            emotionsRepository.Save();
            base.OnClosed(e);
        }
    }
}
