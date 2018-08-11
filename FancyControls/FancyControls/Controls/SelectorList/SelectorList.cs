using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace FancyControls
{
    /// <summary>
    /// Represents a selector which contain unselected items in System.Windows.Forms.ListBox.
    /// </summary>
    [DefaultEvent("ItemSelected")]
    [DefaultProperty("Text")]
    public partial class SelectorList : MoveableControl, ISelectorControl
    {
        // FIELDS
        int middleAreaWidth;
        int middleAreaMargin;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of the FancyControls.SelectorBox class.
        /// </summary>
        public SelectorList(): base()
        {
            MiddleAreaWidth = 90;
            MiddleAreaMargin = 5;
            InitializeComponent();
        }
        /// <summary>
        /// Initializes a new instance of the FancyControls.SelectorList class.
        /// </summary>
        /// <param name="unselectedItems">
        /// An array of unselected items.
        /// </param>
        public SelectorList(params object[] unselectedItems) : base()
        {
            MiddleAreaWidth = 90;
            MiddleAreaMargin = 5;

            InitializeComponent();

            leftLbx.BeginUpdate();
            leftLbx.Items.AddRange(unselectedItems);
            leftLbx.EndUpdate();
        }
        /// <summary>
        /// Initializes a new instance of the FancyControls.SelectorList class.
        /// </summary>
        /// <param name="unselectedItems">
        /// An array of unselected items.
        /// </param>
        /// <param name="selectedItems">
        /// An array of selected items.
        /// </param>
        public SelectorList(object[] unselectedItems, object[] selectedItems) : base()
        {
            MiddleAreaWidth = 90;
            MiddleAreaMargin = 5;

            InitializeComponent();

            leftLbx.BeginUpdate();
            rightLbx.BeginUpdate();

            leftLbx.Items.AddRange(unselectedItems);
            rightLbx.Items.AddRange(selectedItems);

            rightLbx.EndUpdate();
            leftLbx.EndUpdate();

        }
        // PROPERTIES
        /// <summary>
        /// Overrides System.Windows.Forms.Control.Text.
        /// </summary>
        /// <returns>
        /// The text associated with this control.
        /// </returns>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue("Incomplete Tasks")]
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
        /// <summary>
        /// Gets or sets width of middle area in pixels.
        /// </summary>
        [Category("Appearance")]
        [Description("Gets or sets width of middle area in pixels.")]
        [DefaultValue(90)]
        public int MiddleAreaWidth
        {
            get
            {
                return middleAreaWidth;
            }

            set
            {
                if (value > 100 || value < 30)
                {
                    throw new ArgumentException(
                        message:
                            $"Value of '{value}' is not valid for '{nameof(this.MiddleAreaWidth)}'. '{nameof(this.MiddleAreaWidth)}' should be between '30' and '100'.",
                        paramName: nameof(this.MiddleAreaWidth));
                }

                middleAreaWidth = value;
                this.OnResize(EventArgs.Empty);
            }
        }
        /// <summary>
        /// Gets or sets width of middle area margin in pixels.
        /// </summary>
        [Category("Appearance")]
        [Description("Gets or sets width of middle area margin in pixels.")]
        [DefaultValue(5)]
        public int MiddleAreaMargin
        {
            get
            {
                return middleAreaMargin;
            }

            set
            {
                if (value > 10 || value < 0)
                {
                    throw new ArgumentException(
                        message:
                            $"Value of '{value}' is not valid for '{nameof(this.MiddleAreaMargin)}'. '{nameof(this.MiddleAreaMargin)}' should be between '0' and '10'.",
                        paramName: nameof(this.MiddleAreaMargin));
                }

                middleAreaMargin = value;
                this.OnResize(EventArgs.Empty);
            }
        }
        /// <summary>
        /// Gets or sets image on Select button.
        /// </summary>
        [Category("Appearance")]
        [Description("Gets or sets image on Select button.")]
        public Image SelectImage
        {
            set
            {
                this.toRightBtn.Image = value;
                OnSelectImageChanged(EventArgs.Empty);
            }
            get
            {
                return this.toRightBtn.Image;
            }
        }
        /// <summary>
        /// Gets or sets image on Unselect button.
        /// </summary>
        [Category("Appearance")]
        [Description("Gets or sets image on Unselect button.")]
        public Image UnselectImage
        {
            set
            {
                this.toLeftBtn.Image = value;
                OnUnselectImageChanged(EventArgs.Empty);
            }
            get
            {
                return this.toLeftBtn.Image;
            }
        }
        /// <summary>
        /// Gets a collection of selected indices which can be modified.
        /// </summary>
        [Browsable(false)]
        public ListBox.SelectedIndexCollection SelectedIndices => rightLbx.SelectedIndices;
        /// <summary>
        /// Gets a collection of unselected indices which can be modified.
        /// </summary>
        [Browsable(false)]
        public ListBox.SelectedIndexCollection UnselectedIndices => leftLbx.SelectedIndices;

        /// <summary>
        /// Gets a collection of unselected items which can be modified.
        /// </summary>
        [Browsable(false)]
        public ListBox.ObjectCollection UnselectedItems => leftLbx.Items;
        /// <summary>
        /// Gets a collection of selected items which can be modified.
        /// </summary>
        [Browsable(false)]
        public ListBox.ObjectCollection SelectedItems => rightLbx.Items;   

        // METHODS
        /// <summary>
        /// Select an item at specified index.
        /// </summary>
        /// <param name="index">
        /// An index of an item which should be selected.
        /// </param>
        public void SelectAt(int index)
        {
            if (IndexInSelectRange(index))
            {
                rightLbx.Items.Add(leftLbx.Items[index]);
                leftLbx.Items.RemoveAt(index);

                OnItemSelected(new IndexEventArgs(index: index));
            }
        }
        /// <summary>
        /// Select items for a specified array of indices
        /// </summary>
        /// <param name="indices">
        /// The array of indices which items should be selected. 
        /// </param>
        public void SelectAt(params int[] indices)
        {
            indices = indices                                  // get indices
                .Where(index => IndexInSelectRange(index))     // in range
                .Distinct()                                    // unique
                .OrderBy(x => x)                               // sorted
                .ToArray();
            
            leftLbx.BeginUpdate();
            rightLbx.BeginUpdate();

            for (int i = 0; i < indices.Length; ++i)
            {
                // sselect item by index
                // but if item was selected 
                // next item has index = index - iteration amount
                SelectAt(indices[i] - i);
            }

            rightLbx.EndUpdate();
            leftLbx.EndUpdate();
        }
        /// <summary>
        /// Unselect an item at specified index.
        /// </summary>
        /// <param name="index">
        /// An index of an item which should be unselected.
        /// </param>
        public void UnselectAt(int index)
        {
            if (IndexInUnselectRange(index))
            {
                leftLbx.Items.Add(rightLbx.Items[index]);
                rightLbx.Items.RemoveAt(index);

                OnItemUnselected(new IndexEventArgs(index: index));
            }
        }
        /// <summary>
        /// Unselect items for a specified array of indices
        /// </summary>
        /// <param name="indices">
        /// The array of indices which items should be unselected. 
        /// </param>
        public void UnselectAt(int[] indices)
        {
            indices = indices                                  // get indices
                .Where(index => IndexInUnselectRange(index))   // in range
                .Distinct()                                    // unique
                .OrderBy(x => x)                               // sorted
                .ToArray();

            leftLbx.BeginUpdate();
            rightLbx.BeginUpdate();

            for (int i = 0; i < indices.Length; ++i)
            {
                // unselect item by index
                // but if item was unselected 
                // next item has index = index - iteration amount
                UnselectAt(indices[i] - i);
            }

            rightLbx.EndUpdate();
            leftLbx.EndUpdate();
        }


        /// <summary>
        /// Does not clear, it moves all items from selected to unselected.
        /// </summary>
        public void Clean()
        {
            if(rightLbx.Items.Count > 0)
            {
                leftLbx.BeginUpdate();
                rightLbx.BeginUpdate();

                leftLbx.Items.AddRange(rightLbx.Items);
                rightLbx.Items.Clear();

                rightLbx.EndUpdate();
                leftLbx.EndUpdate();
            }
        }
        /// <summary>
        /// Removes all items from the control.
        /// </summary>
        public void Clear()
        {
            leftLbx.Items.Clear();
            rightLbx.Items.Clear();
        }
        private bool IndexInSelectRange(int index)
        {
            return index > -1 && index < leftLbx.Items.Count;
        }
        private bool IndexInUnselectRange(int index)
        {
            return index > -1 && index < rightLbx.Items.Count;
        }

        // EVENTS
        /// <summary>
        /// Occurs when the value of the FancyControls.SelectorList.SelectImage property changes.
        /// </summary>
        [Category("Appearance")]
        [Description("Occurs when the value of the FancyControls.SelectorList.SelectImage property changes.")]
        public event EventHandler SelectImageChanged;
        /// <summary>
        /// Occurs when the value of the FancyControls.SelectorList.UnselectImage property changes.
        /// </summary>
        [Category("Appearance")]
        [Description("Occurs when the value of the FancyControls.SelectorList.UnselectImage property changes.")]
        public event EventHandler UnselectImageChanged;

        /// <summary>
        /// Occurs when the item at specified index is selected.
        /// </summary>
        [Category("Behavior")]
        [Description("Occurs when the item at specified index is selected.")]
        public event EventHandler<IndexEventArgs> ItemSelected;
        /// <summary>
        /// Occurs when the item at specified index is unselected.
        /// </summary>
        [Category("Behavior")]
        [Description("Occurs when the item at specified index is unselected.")]
        public event EventHandler<IndexEventArgs> ItemUnselected;

        // EVENT'S METHODS
        /// <summary>
        /// Raises the FancyControls.SelectorList.SelectImageChanged event.
        /// </summary>
        /// <param name="e">
        /// A System.EventArgs that contains the event data.
        /// </param>
        protected void OnSelectImageChanged(EventArgs e)
        {
            SelectImageChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the FancyControls.SelectorList.UnselectImageChanged event.
        /// </summary>
        /// <param name="e">
        /// A System.EventArgs that contains the event data.
        /// </param>
        protected void OnUnselectImageChanged(EventArgs e)
        {
            UnselectImageChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the FancyControls.SelectorList.ItemSelected event.
        /// </summary>
        /// <param name="e">
        /// A FancyControls.IndexEventArgs that contains the event data.
        /// </param>
        protected void OnItemSelected(IndexEventArgs e)
        {
            ItemSelected?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the FancyControls.SelectorList.ItemUnselected event.
        /// </summary>
        /// <param name="e">
        /// A FancyControls.IndexEventArgs that contains the event data.
        /// </param>
        protected void OnItemUnselected(IndexEventArgs e)
        {
            ItemUnselected?.Invoke(this, e);
        }

        // CLICK
        private void toRightBtn_Click(object sender, EventArgs e)
        {
            SelectAt(UnselectedIndices.OfType<int>().ToArray());
        }

        private void toLeftBtn_Click(object sender, EventArgs e)
        {
            UnselectAt(SelectedIndices.OfType<int>().ToArray());
        }
        
        // BTN ANIMATION
        private void toRightMove_MouseHover(object sender, EventArgs e)
        {
            AnimateMoveControl((Control)sender, +HoverPixelAmount, 0);
        }

        private void toLeftMove_MouseHover(object sender, EventArgs e)
        {
            AnimateMoveControl((Control)sender, -HoverPixelAmount, 0);
        }

        private void toRightMove_MousePressed(object sender, MouseEventArgs e)
        {
            AnimateMoveControl((Control)sender, +PushedPixelAmount, 0);
        }

        private void toLeftMove_MousePressed(object sender, MouseEventArgs e)
        {
            AnimateMoveControl((Control)sender, -PushedPixelAmount, 0);
        }

        private void SelectorList_Resize(object sender, EventArgs e)
        {
            int panelsWidth = (this.Width - MiddleAreaWidth) / 2;

            leftLbx.Width = panelsWidth;
            leftLbx.Height = this.Height;   

            rightLbx.Width = panelsWidth;
            rightLbx.Location = new Point(panelsWidth + MiddleAreaWidth, rightLbx.Location.Y);
            rightLbx.Height = this.Height;

            Size btnSize = new Size(middleAreaWidth / 2 + 10, middleAreaWidth / 3 + 10);// magic 10
            toLeftBtn.Size = btnSize;
            toRightBtn.Size = btnSize;
            toLeftBtn.Location = new Point(leftLbx.Width + MiddleAreaMargin, toLeftBtn.Location.Y);
            toRightBtn.Location = new Point(this.Width - rightLbx.Width - toRightBtn.Width - MiddleAreaMargin, toRightBtn.Location.Y);

            TextLbl.Width = middleAreaWidth - MiddleAreaMargin;
            TextLbl.Location = new Point(leftLbx.Width + MiddleAreaMargin, TextLbl.Location.Y);
        }
    }
}
