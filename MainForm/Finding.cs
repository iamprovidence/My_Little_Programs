using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace SnippetCreation
{
    // FINDING LITERAL IN TEXT
    public partial class MainForm
    {
        private async void ChangeTextColorInBackground()
        {
            while (runFinding)
            {
                FindingForWords();
                await Task.Delay(300);
            }
        }
        // FINDING ALGORITHMS
        private void FindingForWords()
        {
            // запам'ятовуємо початкове положення каретки
            int selectionStartIndex = CodeRichTextBox.SelectionStart;
            int selectionLength = CodeRichTextBox.SelectionLength;

            // для кожного слова в RichTextBox
            string[] WordsInText = CodeRichTextBox.Text.Split();
            int carriagePosition = 0;// індекс позиції каретки

            foreach (string word in WordsInText)
            {
                Literal literal = LiteralList.ContainsReturn(word);
                if (literal != null)// якщо слово є літералом...
                {
                    // ...змінюємо його колір
                    CodeRichTextBox.Select(carriagePosition, word.Length);
                    CodeRichTextBox.SelectionColor = literal.Color;
                }
                else // якщо слово не є літералом
                {
                    // потрібно коли слово переправлено
                    CodeRichTextBox.Select(carriagePosition, word.Length);
                    CodeRichTextBox.SelectionColor = Color.Black;
                }
                // міняємо індекс позиції каретки
                carriagePosition += word.Length + 1;
            }

            // повертаємо каретку в ту позицію в якій вона була
            CodeRichTextBox.Select(selectionStartIndex, selectionLength);
        }
        // TODO: зміна кольору на чорний
        private void FindingForLiterals()
        {
            foreach (Literal literal in LiteralList)
            {
                // запам'ятовуємо початкове положення каретки
                int selectionStartIndex = CodeRichTextBox.SelectionStart;
                int selectionLength = CodeRichTextBox.SelectionLength;

                int start = 0;
                int end = CodeRichTextBox.TextLength;

                string NeedFind = literal.ID;

                // всі слова в чорний колір, якщо якесь слово змінили
                CodeRichTextBox.ForeColor = Color.Black;// TODO

                do
                {
                    // власне пошук і заміна
                    start = CodeRichTextBox.Find(NeedFind, start, CodeRichTextBox.Text.Length, RichTextBoxFinds.MatchCase) + 1;
                    CodeRichTextBox.ScrollToCaret();
                    CodeRichTextBox.SelectionColor = literal.Color;
                } while (start < end && start != 0);

                // повертаємо каретку в ту позицію в якій вона була
                CodeRichTextBox.Select(selectionStartIndex, selectionLength);
            }

        }
    }
}
