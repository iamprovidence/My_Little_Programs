using System;

namespace ActiveConsolePages.IO
{
    public class OutputService
    {
        public void SetTitle(string title)
        {
            Console.Title = title;
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void Write(string str)
        {
            Console.Write(str);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void WriteLine(string str)
        {
            Console.WriteLine(str);
        }

        public void WriteLine(ConsoleColor color, string value)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        public void DisplayPrompt(string str)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(str + ' ');
            Console.ResetColor();
        }

        public void DisplayError(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("[ERROR]: {0}", message);
            Console.ResetColor();
        }
    }
}
