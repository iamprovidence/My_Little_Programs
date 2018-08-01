using System;
using System.Linq;
using System.Windows.Forms;
using AdvancedDictionary.View;
using AdvancedDictionary.Model;
using AdvancedDictionary.Interfaces;
using System.Collections.Specialized;

namespace AdvancedDictionary.UserControls
{
    internal partial class WordsControl : UserControl, ISaveable
    {
        // FIELDS
        Words verbs;
        VerbsView dataView;
        EmotionsRepository repos;
        string filePath;

        // CONSTRUCTORS
        public WordsControl()
        {
            InitializeComponent();
        }
        public void Build(string filePath, EmotionsRepository repos)
        {
            this.filePath = filePath;
            this.repos = repos;
            LoadData();
            this.dataView = new VerbsView(verbs);

            this.dataView.CollectionChanged += DataCollectionChange;
            this.synonymsPourer.Build(new Synonyms(dataView.Verbs.Select(x => x.Text)));
            this.emotionsPourer.Build(new Emotions(repos));

            UpdateInterface();
        }
        
        public void UpdateInterface()
        {
            if (VerbMainDgv.RowCount != dataView.Count)
            {
                VerbMainDgv.RowCount = 0;
                VerbMainDgv.RowCount = dataView.Count;
            }
            VerbMainDgv.Invalidate();
            VerbAmountLbl.Text = dataView.Count.ToString();
        }
        public void SaveToFile()
        {
            Words.Save(verbs, filePath);
        }
        public void LoadFromFile()
        {
            LoadData();

            this.dataView = new VerbsView(verbs);
            dataView.CollectionChanged += DataCollectionChange;
            UpdateInterface();
        }
        private void LoadData()
        {
            try
            {
                Words.Load(out verbs, filePath);
            }
            catch (Exception)
            {
                verbs = new Words();
                MessageBox.Show("Cannot read properly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // EVENTS
        private void VerbMainDgv_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {

            try
            {
                switch (e.ColumnIndex)
                {
                    case 0: e.Value = dataView[e.RowIndex].Text; break;
                    case 1: e.Value = dataView[e.RowIndex].Description; break;
                    case 2: e.Value = dataView[e.RowIndex].Emotions.PickedStr(); break;
                    case 3: e.Value = dataView[e.RowIndex].Synonyms.PickedStr(); break;
                }
            }
            catch ( ArgumentOutOfRangeException)
            {
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void DataCollectionChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateInterface();
        }
            // CLICK
        private void showBtn_Click(object sender, EventArgs e)
        {
            dataView.Build();
            synonymsPourer.Clean();
            emotionsPourer.Clean();
            dataView.Filter.Clear();
            UpdateInterface();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            Forms.AddChangeVerbForm inputForm = new Forms.AddChangeVerbForm(verbs);
            inputForm.Build(repos);

            if(inputForm.ShowDialog() == DialogResult.OK)
            {
                verbs.Add(inputForm.Data);
                synonymsPourer.Add(inputForm.Data.Text);
            }
        }
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if(VerbMainDgv.CurrentRow != null)
            {
                int index = VerbMainDgv.CurrentRow.Index;
                Forms.AddChangeVerbForm inputForm = new Forms.AddChangeVerbForm(verbs);
                inputForm.Build(dataView[index], repos);
                string oldValue = inputForm.Data.Text;

                if(inputForm.ShowDialog() == DialogResult.OK)
                {
                    string newValue = inputForm.Data.Text;
                    UpdateInterface();
                    synonymsPourer.Replace(oldValue, newValue);
                }
            }
        }
        private void removeBtn_Click(object sender, EventArgs e)
        {
            if (VerbMainDgv.CurrentRow != null &&
                MessageBox.Show("Are you sure you want to remove this items?", "Remove",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int index = VerbMainDgv.CurrentRow.Index;
                synonymsPourer.Remove(dataView.Verbs[index].Text);
                dataView.Verbs.RemoveAt(index);
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to remove all items?", "Clear", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question)   == DialogResult.Yes)
            {
                verbs.Clear();
                synonymsPourer.Clear();
            }
        }
        private void sortBtn_Click(object sender, EventArgs e)
        {
            dataView.SortBy(AdditionalClasses.VerbComparer.ComparerType.Text);
            UpdateInterface();
        }

        private void synonymsPourer_PouringToLeft(object sender, AdditionalClasses.EventArgsClasses.PouringEventArgs e)
        {
            dataView.Filter.RemoveSynonym(e.Name);
            dataView.FilterBy(dataView.Filter.Predicate);
            UpdateInterface();
        }

        private void synonymsPourer_PouringToRight(object sender, AdditionalClasses.EventArgsClasses.PouringEventArgs e)
        {
            dataView.Filter.AddSynonym(e.Name);
            dataView.FilterBy(dataView.Filter.Predicate);
            UpdateInterface();
        }

        private void emotionsPourer_PouringToLeft(object sender, AdditionalClasses.EventArgsClasses.PouringEventArgs e)
        {
            dataView.Filter.RemoveEmotion(e.Name);
            dataView.FilterBy(dataView.Filter.Predicate);
            UpdateInterface();
        }

        private void emotionsPourer_PouringToRight(object sender, AdditionalClasses.EventArgsClasses.PouringEventArgs e)
        {
            dataView.Filter.AddEmotion(e.Name);
            dataView.FilterBy(dataView.Filter.Predicate);
            UpdateInterface();
        }
    }
}
