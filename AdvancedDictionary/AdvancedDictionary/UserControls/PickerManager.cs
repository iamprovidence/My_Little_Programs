using System;
using System.Windows.Forms;
using static AdvancedDictionary.AdditionalClasses.Constants;

namespace AdvancedDictionary.UserControls
{
    public partial class PickerManager : UserControl
    {
        
        // FIELDS
        Interfaces.IDescriptionWordView<string> data;
        protected int btnHoverPixel;
        protected int btnPushedPixel;

        // PROPETIES
        public int BtnHoverPixel
        {
            get
            {
                return btnHoverPixel;
            }

            set
            {
                btnHoverPixel = value;
            }
        }
        public int BtnPushedPixel
        {
            get
            {
                return btnPushedPixel;
            }

            set
            {
                btnPushedPixel = value;
            }
        }
        public Interfaces.IDescriptionWordView<string> Value => data;
        // CONSTRUCTORS
        public PickerManager()
        {
            InitializeComponent();
        }
        // METHODS
        public void Build(Interfaces.IDescriptionWordView<string> data)
        {
            this.data = data;
            foreach(string s in data.Picked)
            {
                listBox.Items.Add(s);
            }
            foreach(string s in data.Unpicked)
            {
                comboBox.Items.Add(s);
            }
        }
        protected void MoveControl(Control control, int offsetX, int offsetY)
        {
            control.Location = new System.Drawing.Point(control.Location.X + offsetX, control.Location.Y + offsetY);
        }

        // EVENT
        protected override void OnLoad(EventArgs e)
        {
            BtnHoverPixel = 1;
            BtnPushedPixel = 3;
            base.OnLoad(e);
        }
        // CLICK
        private void addBtn_Click(object sender, EventArgs e)
        {
            int selectedIndex = comboBox.SelectedIndex;
            if (selectedIndex != WRONG_INDEX)
            {
                listBox.Items.Add( comboBox.Items[selectedIndex] );
                comboBox.Items.RemoveAt(selectedIndex);

                data.Pick(selectedIndex);
            }
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection indices = listBox.SelectedIndices;

            while (indices.Count != EMPTY_COLLECTION_SIZE)
            {
                comboBox.Items.Add(listBox.Items[indices[0]]);
                data.Unpick(indices[0]);
                listBox.Items.RemoveAt(indices[0]);                
            }
        }
            // BTN ANIMATION
        private void Btn_MouseEnter(object sender, EventArgs e)
        {
            MoveControl((Button)sender, 0, BtnHoverPixel);
        }
        private void Btn_MouseLeave(object sender, EventArgs e)
        {
            MoveControl((Button)sender, 0, -BtnHoverPixel);
        }
        private void Btn_MouseDown(object sender, MouseEventArgs e)
        {
            MoveControl((Button)sender, 0, BtnPushedPixel);
        }
        private void Btn_MouseUp(object sender, MouseEventArgs e)
        {
            MoveControl((Button)sender, 0, -BtnPushedPixel);
        }        
    }
}
