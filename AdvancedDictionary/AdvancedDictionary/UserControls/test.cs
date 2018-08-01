using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvancedDictionary.UserControls
{
    internal partial class test : UserControl
    {
            // FIELDS
            Model.Verbs verbs;
            View.VerbsView data;

            public test()
            {
                InitializeComponent();
            }
            public void Build(View.VerbsView data)
            {
                this.data = data;
                verbs = data.Verbs;
                data.CollectionChanged += DataCollectionChange;
            }



            public void UpdateInterface()
            {
                if (VerbMainDgv.RowCount != data.Count)
                {
                    VerbMainDgv.RowCount = 0;
                    VerbMainDgv.RowCount = data.Count;
                }
                VerbMainDgv.Invalidate();
                VerbAmountLbl.Text = data.Count.ToString();
            }

            // EVENTS
            private void VerbMainDgv_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
            {
                switch (e.ColumnIndex)
                {
                    case 0: e.Value = data[e.RowIndex].Text; break;
                    case 1: e.Value = data[e.RowIndex].Description; break;
                    case 2: /*e.Value = data[e.RowIndex].Emotions; */   break;
                    case 3: /*e.Value = data[e.RowIndex].Synonyms; */   break;
                }
            }

            private void VerbMainDgv_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
            {
                switch (e.ColumnIndex)
                {
                    case 0: data[e.RowIndex].Text = e.Value.ToString(); break;
                    case 1: data[e.RowIndex].Description = e.Value.ToString(); break;
                    case 2:/* data[e.RowIndex].Emotions = e.Value; */   throw new NotImplementedException(); break;
                    case 3:/* data[e.RowIndex].Synonyms = e.Value;  */    throw new NotImplementedException(); break;
                }
            }
            private void DataCollectionChange(object sender, EventArgs e)
            {
                UpdateInterface();
            }
            // CLICK
            private void showBtn_Click(object sender, EventArgs e)
            {
                data.Build();
                this.UpdateInterface();
            }

            private void addBtn_Click(object sender, EventArgs e)
            {
                Forms.AddChangeVerbForm inputForm = new Forms.AddChangeVerbForm(verbs);
                inputForm.Build();
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    verbs.Add(inputForm.Data);
                }
            }
            private void EditBtn_Click(object sender, EventArgs e)
            {
                if (VerbMainDgv.CurrentRow != null)
                {
                    int index = VerbMainDgv.CurrentRow.Index;
                    Forms.AddChangeVerbForm inputForm = new Forms.AddChangeVerbForm(verbs);
                    inputForm.Build(data[index]);
                    inputForm.ShowDialog();
                }
            }
        private void removeBtn_Click(object sender, EventArgs e)
        {

        }
       }
}
