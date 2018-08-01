using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using AdvancedDictionary.Interfaces;
using AdvancedDictionary.AdditionalClasses.EventArgsClasses;
using static AdvancedDictionary.AdditionalClasses.Constants;

namespace AdvancedDictionary.UserControls
{
    public partial class Pourer : UserControl
    {
        // FIELDS
        IDescriptionWordList<string> data;
        int btnHoverPixel;
        int btnPushedPixel;
        int middleAreaWidth;

        // PROPERTIES
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
        public override string Text
        {
            get
            {
                return TextLbl.Text;
            }

            set
            {
                TextLbl.Text = value;
            }
        }
        public int MiddleAreaWidth
        {
            get
            {
                return middleAreaWidth;
            }

            set
            {
                middleAreaWidth = value;
            }
        }
        // CONSTRUCTORS
        public Pourer()
        {
            BtnHoverPixel = 1;
            BtnPushedPixel = 3;
            MiddleAreaWidth = 90;
            InitializeComponent();
        }

        public void UpdateInterface()
        {
            leftLbx.Items.Clear();
            rightLbx.Items.Clear();
            foreach (string i in data.Unpicked)
            {
                leftLbx.Items.Add(i);
            }
            foreach (string i in data.Picked)
            {
                rightLbx.Items.Add(i);
            }
        }
        // METHODS
        public void Build(IDescriptionWordList<string> data)
        {
            this.data = data;
            UpdateInterface();
        }
        /// <summary>
        /// Does not clear, just move all element from right to left
        /// </summary>
        public void Clean()
        {
            data.Unpicked.AddRange(data.Picked);
            data.Picked.Clear();
            UpdateInterface();
        }
        public void Add(object item)
        {
            data.Add(item.ToString());
            leftLbx.Items.Add(item);
        }
        public void Remove(object item)
        {
            data.Picked.Remove(item.ToString());
            leftLbx.Items.Remove(item);
        }
        public void Replace(object oldValue, object newValue)
        {
            data.Replace(oldValue.ToString(), newValue.ToString());
            int index = leftLbx.Items.IndexOf(oldValue);
            if(index != WRONG_INDEX)
            {
                leftLbx.Items[index] = newValue;
                return;
            }
            index = rightLbx.Items.IndexOf(oldValue);
            if(index != WRONG_INDEX)
            {
                rightLbx.Items[index] = newValue;
            }

        }
        /// <summary>
        /// Removes all items
        /// </summary>
        public void Clear()
        {
            data.Clear();
            UpdateInterface();
        }
        protected void MoveControl(Control control, int offsetX, int offsetY)
        {
            control.Location = new System.Drawing.Point(control.Location.X + offsetX, control.Location.Y + offsetY);
        }
        

        // EVENTS

        public event EventHandler<PouringEventArgs> PouringToLeft;
        public event EventHandler<PouringEventArgs> PouringToRight;

        private void toRightBtn_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection indices = leftLbx.SelectedIndices;

            while (indices.Count != EMPTY_COLLECTION_SIZE)
            {
                OnPouringRight(new PouringEventArgs("right", data.Unpicked[indices[0]]));

                data.Pick(indices[0]);
                rightLbx.Items.Add(leftLbx.Items[indices[0]]);
                leftLbx.Items.RemoveAt(indices[0]);
            }
        }

        private void toLeftBtn_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection indices = rightLbx.SelectedIndices;

            while (indices.Count != EMPTY_COLLECTION_SIZE)
            {
                OnPouringLeft(new PouringEventArgs("left", data.Picked[indices[0]]));

                data.Unpick(indices[0]);
                leftLbx.Items.Add(rightLbx.Items[indices[0]]);
                rightLbx.Items.RemoveAt(indices[0]);
            }
        }
        protected void OnPouringLeft(PouringEventArgs e)
        {
            PouringToLeft?.Invoke(this, e);
        }
        protected void OnPouringRight(PouringEventArgs e)
        {
            PouringToRight?.Invoke(this, e);
        }
        // BTN ANIMATION
        private void toRightMove_MouseHover(object sender, EventArgs e)
        {
            MoveControl((Button)sender, +BtnHoverPixel, 0);
        }

        private void toLeftMove_MouseHover(object sender, EventArgs e)
        {
            MoveControl((Button)sender, -BtnHoverPixel, 0);
        }

        private void toRightMove_MousePressed(object sender, MouseEventArgs e)
        {
            MoveControl((Button)sender, +BtnPushedPixel, 0);
        }

        private void toLeftMove_MousePressed(object sender, MouseEventArgs e)
        {
            MoveControl((Button)sender, -BtnPushedPixel, 0);
        }

        private void Pourer2_Resize(object sender, EventArgs e)
        {
            int panelsWidth = (this.Width - MiddleAreaWidth) / 2;

            leftLbx.Width = panelsWidth;
            leftLbx.Height = this.Height;

            rightLbx.Width = panelsWidth;
            rightLbx.Location = new Point(panelsWidth + MiddleAreaWidth, rightLbx.Location.Y);
            rightLbx.Height = this.Height;
        }
    }
}
