using System;

namespace ActiveConsolePages.IO
{
    public class InputService
    {
        private readonly OutputService _output;

        public InputService(OutputService output)
        {
            _output = output;
        }

        public void ReadAny()
        {
            Console.ReadKey();
        }

        public int ReadInt(int min, int max)
        {
            var value = ReadInt();

            while (value < min || value > max)
            {
                _output.DisplayPrompt($"Please enter an integer between {min} and {max} (inclusive) :");
                value = ReadInt();
            }

            return value;
        }

        public int ReadInt()
        {
            var input = Console.ReadLine();
            int value;

            while (!int.TryParse(input, out value))
            {
                _output.DisplayPrompt("Please enter an integer :");
                input = Console.ReadLine();
            }

            return value;
        }
    }
}
