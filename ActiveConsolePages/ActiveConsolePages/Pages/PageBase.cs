using System;
using System.Linq;
using ActiveConsolePages.IO;
using ActiveConsolePages.Navigation;

namespace ActiveConsolePages.Pages
{
    public abstract class PageBase
    {
        public PageContext PageContext { get; internal set; }
        public NavigationService Navigation => new NavigationService(PageContext);
        public InputService Input => new InputService(Output);
        public OutputService Output => new OutputService();

        public abstract string Title { get; }

        internal void Initialize(PageContext pageContext)
        {
            PageContext = pageContext;
        }
        public abstract void HandleInput();

        public virtual void Display()
        {
            Output.SetTitle(Title);

            Output.Clear();

            Output.WriteLine("_______________________________________________________________________________________________________________________\n");
            Output.WriteLine(ConsoleColor.Cyan, GetHeader());
            Output.WriteLine("_______________________________________________________________________________________________________________________\n");
        }

        private string GetHeader()
        {
            if (PageContext.History.Count > 1 && PageContext.EnableBreadcrumbs)
            {
                var history = PageContext.History.Select(page => page.Title).Reverse();

                return string.Join(" > ", history);
            }

            return Title;
        }
    }
}
