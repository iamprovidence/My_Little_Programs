using System.Drawing;
using System.Windows.Forms;

namespace FancyControls
{
    internal static class Algorithms
    {
        internal static void FitTextByWidth(Label label)
        {
            while (label.Width < TextRenderer.MeasureText(label.Text, label.Font).Width)
            {
                label.Font = new Font(label.Font.FontFamily, label.Font.Size - 0.5f, label.Font.Style);
            }
        }
        internal static void ForEachControl<ControlType>(Control control, System.Action<ControlType> action) where ControlType: Control
        {
            if(!control.HasChildren && control is ControlType)
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
