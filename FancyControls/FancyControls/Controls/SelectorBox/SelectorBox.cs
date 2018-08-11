using System;
using System.Linq;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace FancyControls
{
    [DefaultEvent("ItemSelected")]
    [DefaultProperty("Name")]
    public partial class SelectorBox : MoveableControl, ISelectorControl
    {
        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of the FancyControls.SelectorBox class.
        /// </summary>
        public SelectorBox(): base()
        {
            InitializeComponent();            
        }
        /// <summary>
        /// Initializes a new instance of the FancyControls.SelectorBox class.
        /// </summary>
        /// <param name="unselectedItems">
        /// An array of unselected items.
        /// </param>
        public SelectorBox(params object[] unselectedItems) : base()
        {
            InitializeComponent();

            comboBox.BeginUpdate();
            comboBox.Items.AddRange(unselectedItems);
            comboBox.EndUpdate();
        }
        /// <summary>
        /// Initializes a new instance of the FancyControls.SelectorBox class.
        /// </summary>
        /// <param name="unselectedItems">
        /// An array of unselected items.
        /// </param>
        /// <param name="selectedItems">
        /// An array of selected items.
        /// </param>
        public SelectorBox(object[] unselectedItems, object[] selectedItems) : base()
        {
            InitializeComponent();

            comboBox.BeginUpdate();
            listBox.BeginUpdate();

            comboBox.Items.AddRange(unselectedItems);
            listBox.Items.AddRange(selectedItems);

            listBox.EndUpdate();
            comboBox.EndUpdate();
        }
        // PROPERTIES
        /// <summary>
        /// Gets or sets image on Add button.
        /// </summary>
        [Category("Appearance")]
        [Description("Gets or sets image on Add button.")]
        public Image AddImage
        {
            set
            {
                this.addBtn.Image = value;
                OnAddImageChanged(EventArgs.Empty);
            }
            get
            {
                return this.addBtn.Image;
            }
        }
        /// <summary>
        /// Gets or sets image on Remove button.
        /// </summary>
        [Category("Appearance")]
        [Description("Gets or sets image on Remove button.")]
        public Image RemoveImage
        {
            set
            {
                this.removeBtn.Image = value;
                OnRemoveImageChanged(EventArgs.Empty);
            }
            get
            {
                return this.removeBtn.Image;
            }
        }
        /// <summary>
        /// Gets selected index.
        /// </summary>
        [Browsable(false)]
        public int UnselectedIndex => comboBox.SelectedIndex;
        /// <summary>
        /// Gets a collection of unselected indices which can be modified.
        /// </summary>
        [Browsable(false)]
        public ListBox.SelectedIndexCollection SelectedIndices => listBox.SelectedIndices;

        /// <summary>
        /// Gets a collection of unselected items which can be modified.
        /// </summary>
        [Browsable(false)]
        public ComboBox.ObjectCollection UnselectedItems => comboBox.Items;
        /// <summary>
        /// Gets a collection of selected items which can be modified.
        /// </summary>
        [Browsable(false)]
        public ListBox.ObjectCollection SelectedItems => listBox.Items;

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
                listBox.Items.Add(comboBox.Items[index]);
                comboBox.Items.RemoveAt(index);

                OnItemSelected(new IndexEventArgs(index: index));
            }
        }
        /// <summary>
        /// Unelect an item at specified index.
        /// </summary>
        /// <param name="index">
        /// An index of an item which should be unselected.
        /// </param>
        public void UnselectAt(int index)
        {
            if (IndexInUnselectRange(index))
            {
                comboBox.Items.Add(listBox.Items[index]);
                listBox.Items.RemoveAt(index);

                OnItemUnselected(new IndexEventArgs(index: index));
            }
        }
        /// <summary>
        /// Unselect items for a specified array of indices
        /// </summary>
        /// <param name="indices">
        /// The array of indices which items should be unselected. 
        /// </param>
        public void UnselectAt(params int[] indices)
        { 
            indices = indices                                  // get indices
                .Where(index => IndexInUnselectRange(index))   // in range
                .Distinct()                                    // unique
                .OrderBy(x => x)                               // sorted
                .ToArray();

            comboBox.BeginUpdate();
            listBox.BeginUpdate();

            for(int i = 0; i < indices.Length; ++ i)
            {
                // unselect item by index
                // but if item was unselected 
                // next item has index = index - iteration amount
                UnselectAt(indices[i] - i);
            }

            listBox.EndUpdate();
            comboBox.EndUpdate();
        }
        /// <summary>
        /// Does not clear, it moves all items from selected to unselected.
        /// </summary>
        public void Clean()
        {
            if(listBox.Items.Count > 0)
            {
                comboBox.BeginUpdate();
                listBox.BeginUpdate();

                comboBox.Items.AddRange(listBox.Items.OfType<object>().ToArray());
                listBox.Items.Clear();

                listBox.EndUpdate();
                comboBox.EndUpdate();
            }
        }
        /// <summary>
        /// Removes all items from the control.
        /// </summary>
        public void Clear()
        {
            comboBox.Items.Clear();
            listBox.Items.Clear();
        }
        private bool IndexInSelectRange(int index)
        {
            return index > -1 && index < comboBox.Items.Count;
        }
        private bool IndexInUnselectRange(int index)
        {
            return index > -1 && index < listBox.Items.Count;
        }
        // EVENTS
        /// <summary>
        /// Occurs when the value of the FancyControls.SelectorBox.AddImage property changes.
        /// </summary>
        [Category("Appearance")]
        [Description("Occurs when the value of the FancyControls.SelectorBox.AddImage property changes.")]
        public event EventHandler AddImageChanged;
        /// <summary>
        /// Occurs when the value of the FancyControls.SelectorBox.RemoveImage property changes.
        /// </summary>
        [Category("Appearance")]
        [Description("Occurs when the value of the FancyControls.SelectorBox.RemoveImage property changes.")]
        public event EventHandler RemoveImageChanged;

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
        /// Raises the FancyControls.SelectorBox.AddImageChanged event.
        /// </summary>
        /// <param name="e">
        /// A System.EventArgs that contains the event data.
        /// </param>
        protected void OnAddImageChanged(EventArgs e)
        {
            AddImageChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the FancyControls.SelectorBox.RemoveImageChanged event.
        /// </summary>
        /// <param name="e">
        /// A System.EventArgs that contains the event data.
        /// </param>
        protected void OnRemoveImageChanged(EventArgs e)
        {
            RemoveImageChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the FancyControls.SelectorBox.ItemSelected event.
        /// </summary>
        /// <param name="e">
        /// A FancyControls.IndexEventArgs that contains the event data.
        /// </param>
        protected void OnItemSelected(IndexEventArgs e)
        {
            ItemSelected?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the FancyControls.SelectorBox.ItemUnselected event.
        /// </summary>
        /// <param name="e">
        /// A FancyControls.IndexEventArgs that contains the event data.
        /// </param>
        protected void OnItemUnselected(IndexEventArgs e)
        {
            ItemUnselected?.Invoke(this, e);
        }

        // CLICK
        private void addBtn_Click(object sender, EventArgs e)
        {
            SelectAt(comboBox.SelectedIndex);
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            UnselectAt(listBox.SelectedIndices.OfType<int>().ToArray());
        }
        // BTN ANIMATION
        private void Btn_MouseEnter(object sender, EventArgs e)
        {
            AnimateMoveControl((Control)sender, 0, HoverPixelAmount);
        }
        private void Btn_MouseLeave(object sender, EventArgs e)
        {
            AnimateMoveControl((Control)sender, 0, -HoverPixelAmount);
        }
        private void Btn_MouseDown(object sender, MouseEventArgs e)
        {
            AnimateMoveControl((Control)sender, 0, PushedPixelAmount);
        }
        private void Btn_MouseUp(object sender, MouseEventArgs e)
        {
            AnimateMoveControl((Control)sender, 0, -PushedPixelAmount);
        }       
    }
}