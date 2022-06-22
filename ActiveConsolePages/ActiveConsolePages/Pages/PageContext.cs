using System;
using System.Collections.Generic;

namespace ActiveConsolePages.Pages
{
    public class PageContext
    {
        public bool EnableBreadcrumbs { get; internal init; }

        public IServiceProvider ServiceProvider { get; internal init; }

        public bool CanGoBack => History.Count > 1;

        internal Stack<PageBase> History { get; } = new Stack<PageBase>();

        internal PageBase CurrentPage => History.Peek();
    }
}
