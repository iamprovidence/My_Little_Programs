using ActiveConsolePages.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace ActiveConsolePages.Navigation
{
    public class NavigationService
    {
        private readonly PageContext _pageContext;

        internal NavigationService(PageContext pageContext)
        {
            _pageContext = pageContext;
        }

        public void GoHome()
        {
            while (_pageContext.CanGoBack)
            {
                _pageContext.History.Pop();
            }

            _pageContext.CurrentPage.Output.Clear();
            _pageContext.CurrentPage.Display();
        }

        public void GoTo<TPage>()
            where TPage : PageBase
        {
            using (var scope = _pageContext.ServiceProvider.CreateScope())
            {
                var newPage = scope.ServiceProvider.GetRequiredService<TPage>();
                newPage.Initialize(_pageContext);

                _pageContext.History.Push(newPage);
            }

            _pageContext.CurrentPage.Output.Clear();
            _pageContext.CurrentPage.Display();

        }

        public void GoBack()
        {
            if (_pageContext.CanGoBack)
            {
                _pageContext.History.Pop();

                _pageContext.CurrentPage.Output.Clear();
                _pageContext.CurrentPage.Display();
            }
        }
    }
}
