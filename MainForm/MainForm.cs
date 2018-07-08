using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnippetCreation
{
    public partial class MainForm : Form
    {
        // FORM EVENTS
        private void TreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode node in e.Node.Nodes) // для кожного дочірнього вузла
            {
                node.Checked = e.Node.Checked; // встановлюємо відповідний стан Checked
            }
        }
        

        // METHODS
        private async void InvalidBox(dynamic sender)
        {
            // якщо це білий колір, то ...
            if (sender.BackColor.ToArgb() == Color.White.ToArgb())
            {
                // ... можна запускати анімацію
                int BlueGreen = 180;
                while (BlueGreen != 256)
                {
                    sender.BackColor = Color.FromArgb(255, BlueGreen, BlueGreen);
                    ++BlueGreen;
                    await Task.Delay(25);
                }
            }
        }
        private Color NextColor()
        {
            colorIndex = (colorIndex == ArrColor.Length - 1) ? 0 : colorIndex + 1;
            return ArrColor[colorIndex];
        }
            // BTN CLICK
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Add_btn_Click(object sender, EventArgs e)
        {
            string selectedValue = CodeRichTextBox.SelectedText.Trim();

            if (!string.IsNullOrWhiteSpace(selectedValue) && // якщо це НЕ порожнє слово і ...
                ListBox.NoMatches == LiteralListBox.FindStringExact(selectedValue))// ...такого слова немає в списку, то ...
            {
                // ... додаємо
                LiteralList.Add(new Literal(defaultValueForEach: selectedValue, color: NextColor()));
                LiteralListBox.Items.Add(selectedValue);
            }
        }
        private void Remove_btn_Click(object sender, EventArgs e)
        {
            if (LiteralListBox.SelectedItem != null)
            {
                LiteralList.RemoveAt(LiteralListBox.SelectedIndex);
                LiteralListBox.Items.RemoveAt(LiteralListBox.SelectedIndex);
            }
        }
        private void create_btn_Click(object sender, EventArgs e)
        {
            bool isAllOk = true;
            if (String.IsNullOrWhiteSpace(ShortcutTextBox.Text))
            {
                InvalidBox(ShortcutTextBox);
                isAllOk &= false;
            }
            if (String.IsNullOrWhiteSpace(CodeRichTextBox.Text))
            {
                InvalidBox(CodeRichTextBox);
                isAllOk &= false;
            }

            if (isAllOk && saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FormSnippet(saveFileDialog.FileName);
            }
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            create_btn.PerformClick();
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ReadSnippet(openFileDialog.FileName);
            }
        }
        // зміна значення літерала на формі, міняє значення в контейнері
        private void LitetalListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LiteralListBox.SelectedItem != null)// якщо вибрано елемент, то ...
            {
                // ... встановлюємо для полів відповідні значення
                IdTextBox.Text = LiteralList[LiteralListBox.SelectedIndex].ID;
                ToolTipTextBox.Text = LiteralList[LiteralListBox.SelectedIndex].ToolTip;
                DefaultTextBox.Text = LiteralList[LiteralListBox.SelectedIndex].DefaultValue;
                literalColorPanel.BackColor = LiteralList[LiteralListBox.SelectedIndex].Color;
            }
            else // якщо елемент не вибрано, то ...
            {
                // ...очищаємо всі поля
                IdTextBox.Text = ToolTipTextBox.Text = DefaultTextBox.Text = "";
            }
            
        }
        private void IdTextBox_TextChanged(object sender, EventArgs e)
        {
            if (LiteralListBox.SelectedItem != null)
            {
                LiteralList[LiteralListBox.SelectedIndex].ID = IdTextBox.Text;
                LiteralListBox.Items[LiteralListBox.SelectedIndex] = IdTextBox.Text;
            }
        }
        private void ToolTipTextBox_TextChanged(object sender, EventArgs e)
        {
            if (LiteralListBox.SelectedItem != null)
                LiteralList[LiteralListBox.SelectedIndex].ToolTip = ToolTipTextBox.Text;
        }
        private void DefaultTextBox_TextChanged(object sender, EventArgs e)
        {
            if (LiteralListBox.SelectedItem != null)
                LiteralList[LiteralListBox.SelectedIndex].DefaultValue = DefaultTextBox.Text;
        }
        private void literalColorPanel_Click(object sender, EventArgs e)
        {
            if (LiteralListBox.SelectedItem != null)
            {
                if(colorDialog.ShowDialog() == DialogResult.OK)
                {
                    LiteralList[LiteralListBox.SelectedIndex].Color = colorDialog.Color;
                    literalColorPanel.BackColor = LiteralList[LiteralListBox.SelectedIndex].Color;
                }
            }
        }
    }
}
