using System.Windows.Forms;

namespace Extensions
{
    /// <summary>
    /// Class that provide some extensions for <see cref="System.Windows.Forms"/>.
    /// </summary>
    public static class WindowsFormExtensions
    {
        /// <summary>
        /// Apply an action for each control of certain type.
        /// </summary>
        /// <typeparam name="ControlType">The type of the control that should be applied by the action.</typeparam>
        /// <param name="control">The instance of <see cref="System.Windows.Forms.Control"/> that has been extended.</param>
        /// <param name="action">The action that shall be applied to all the elements.</param>
        public static void ForEachControl<ControlType>(Control control, System.Action<ControlType> action) where ControlType : Control
        {
            if (!control.HasChildren && control is ControlType)
            {
                action((ControlType)control);
            }
            else
            {
                foreach (Control c in control.Controls)
                {
                    ForEachControl<ControlType>(c, action);
                }
            }
        }
    }
}
