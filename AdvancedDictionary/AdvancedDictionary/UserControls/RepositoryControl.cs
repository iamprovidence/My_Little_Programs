using System;
using System.Windows.Forms;
using AdvancedDictionary.View;
using AdvancedDictionary.Model;
using AdvancedDictionary.Interfaces;
using static AdvancedDictionary.AdditionalClasses.Constants;

namespace AdvancedDictionary.UserControls
{
    public partial class RepositoryControl : UserControl, ISaveable
    {
        // FIELDS
        IRepositoryView<string, IRepositoryCollection<string>> dataView;
        IRepositoryCollection<string> repos;
        string filePath;
        // PROPERTIES
        public new string Text
        {
            get
            {
                return mainTextLbl.Text;
            }
            set
            {
                mainTextLbl.Text = value;
            }
        }
        // CONSTRUCTORS
        public RepositoryControl()
        {
            InitializeComponent();
        }
        public void Build(string filePath, IRepositoryCollection<string> data)
        {
            this.filePath = filePath;
            repos = data;
            Initialize();
        }
        public void SaveToFile()
        {
            repos.Save();
        }
        public void LoadFromFile()
        {
            throw new NotImplementedException();
        }
        private void Initialize()
        {
            this.dataView = new RepositoryView<string, IRepositoryCollection<string>>(repos);

            this.Text = dataView.Name;
            foreach (string s in dataView.repository)
            {
                itemsLb.Items.Add(s);
            }
        }
        // EVENTS
        private void addBtn_Click(object sender, EventArgs e)
        {
            string text = addTb.Text.Trim();
            if(text != string.Empty)
            {
                if(dataView.repository.Add(text))
                {
                    addTb.Clear();
                    itemsLb.Items.Add(text);
                }
            }
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            int index = itemsLb.SelectedIndex;
            string text = editTb.Text.Trim();
            if (index != WRONG_INDEX && text != string.Empty)
            {
                dataView.repository[index] = text;
                itemsLb.Items[index] = text;
                editTb.Clear();
            }
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to remove this items?", "Remove",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ListBox.SelectedIndexCollection indices = itemsLb.SelectedIndices;

                int count = indices.Count;
                while (indices.Count != EMPTY_COLLECTION_SIZE)
                {
                    dataView.repository.RemoveAt(indices[0]);
                    itemsLb.Items.RemoveAt(indices[0]);
                }
            }

        }

        private void itemsLb_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = itemsLb.SelectedIndex;
            if (index != WRONG_INDEX)
            {
                editTb.Text = itemsLb.Items[index].ToString();
            }
        }

    }
}
