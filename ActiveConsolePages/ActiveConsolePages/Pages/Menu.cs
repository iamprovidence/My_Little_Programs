using System;
using System.Collections.Generic;
using System.Linq;
using ActiveConsolePages.IO;

namespace ActiveConsolePages.Pages
{
    public class Menu : Dictionary<string, Action>
    {
        public void Display(OutputService outputService)
        {
            for (var i = 0; i < Count; i++)
            {
                outputService.WriteLine($"{i + 1}. {this[i].Name}");
            }
        }

        public (string Name, Action Callback) this[int index]
        {
            get
            {
                (string Name, Action Callback) = this.ElementAt(index);

                return (Name, Callback);
            }
        }
    }
}
