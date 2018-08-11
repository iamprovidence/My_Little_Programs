using System.ComponentModel;
using System.Windows.Forms;

namespace FancyControls
{
    /// <summary>
    /// Provide abstract class for control which elements can be moved.
    /// </summary>
    [TypeDescriptionProvider(typeof(AbstractControlDescriptionProvider<MoveableControl, UserControl>))]
    public abstract class MoveableControl : UserControl
    {
        // FIELDS
        /// <summary>
        /// The amount of pixels on which cotrol's elements will be moved when the mouse hover them. 
        /// </summary>
        protected int hoverPixelAmount;
        /// <summary>
        /// The amount of pixels on which cotrol's elements will be moved when the mouse pushed on them.
        /// </summary>
        protected int pushedPixelAmount;
        /// <summary>
        /// Is moving wonrk on control's element.
        /// </summary>
        protected bool canMove;

        // PROPETIES
        /// <summary>
        /// Gets or sets the amount of pixels on which cotrol' elements will be moved when the mouse hover them. 
        /// </summary>
        [Category("Appearance")]
        [Description("Gets or sets the amount of pixels on which cotrol will be moved when the mouse hover him.")]
        [DefaultValue(1)]
        public int HoverPixelAmount
        {
            get
            {
                return hoverPixelAmount;
            }

            set
            {
                hoverPixelAmount = value;
                OnHoverPixelAmountChanged(System.EventArgs.Empty);
            }
        }
        /// <summary>
        /// Gets or sets the amount of pixels on which cotrol's elements will be moved when the mouse pushed on them. 
        /// </summary>
        [Category("Appearance")]
        [Description("Gets or sets the amount of pixels on which cotrol will be moved when the mouse pushed on him.")]
        [DefaultValue(3)]
        public int PushedPixelAmount
        {
            get
            {
                return pushedPixelAmount;
            }

            set
            {
                pushedPixelAmount = value;
                OnPushedPixelAmountChanged(System.EventArgs.Empty);
            }
        }
        /// <summary>
        /// Gets or sets the value if control's elements can be moved.
        /// </summary>
        [Category("Appearance")]
        [Description("Gets or sets the value if control's elements can be moved.")]
        [DefaultValue(true)]
        public bool CanMoveElements
        {
            get
            {
                return canMove;
            }

            set
            {
                canMove = value;
                OnCanMoveElementsChanged(System.EventArgs.Empty);
            }
        }
        // CONSTRUCTORS
        /// <summary>
        /// 
        /// </summary>
        public MoveableControl()
        {
            hoverPixelAmount = 1;
            pushedPixelAmount = 3;
            canMove = true;
        }

        // EVENTS
        /// <summary>
        /// Occurs when the value of FancyControls.MoveableControl.HoverPixelAmount property changes.
        /// </summary>
        [Category("Appearance")]
        [Description("Occurs when the value of FancyControls.HoverPixelAmount property changes.")]
        public System.EventHandler HoverPixelAmountChanged;
        /// <summary>
        /// Occurs when the value of FancyControls.MoveableControl.PushedPixelAmount property changes.
        /// </summary>
        [Category("Appearance")]
        [Description("Occurs when the value of FancyControls.PushedPixelAmount property changes.")]
        public System.EventHandler PushedPixelAmountChanged;
        /// <summary>
        /// Occurs when the value of FancyControls.MoveableControl.CanMove property changes.
        /// </summary>
        [Category("Appearance")]
        [Description("Occurs when the value of FancyControls.CanMove property changes.")]
        public System.EventHandler CanMoveElementsChanged;

        // METHODS
        
        /// <summary>
        /// Move the control on X and Y axises if it is possible.
        /// </summary>
        /// <param name="control">
        /// The control which will be moved.
        /// </param>
        /// <param name="offsetX">
        /// The amount of pixels on which cotrol will be moved on X axis.
        /// </param>
        /// <param name="offsetY">
        /// The amount of pixels on which cotrol will be moved on Y axis.
        /// </param>
        protected void AnimateMoveControl(Control control, int offsetX, int offsetY)
        {
            if (canMove)
            {
                control.Location = new System.Drawing.Point(control.Location.X + offsetX, control.Location.Y + offsetY);
            }
        }
        /// <summary>
        /// Raises the FancyControls.MoveableControl.PushedPixelAmountChanged event.
        /// </summary>
        /// <param name="e">
        /// A System.EventArgs that contains the event data.
        /// </param>
        protected void OnPushedPixelAmountChanged(System.EventArgs e)
        {
            PushedPixelAmountChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the FancyControls.MoveableControl.HoverPixelAmountChanged event.
        /// </summary>
        /// <param name="e">
        /// A System.EventArgs that contains the event data.
        /// </param>
        protected void OnHoverPixelAmountChanged(System.EventArgs e)
        {
            HoverPixelAmountChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the FancyControls.MoveableControl.CanMoveElementsChanged event.
        /// </summary>
        /// <param name="e">
        /// A System.EventArgs that contains the event data.
        /// </param>
        protected void OnCanMoveElementsChanged(System.EventArgs e)
        {
            HoverPixelAmountChanged?.Invoke(this, e);
        }
    }
}
