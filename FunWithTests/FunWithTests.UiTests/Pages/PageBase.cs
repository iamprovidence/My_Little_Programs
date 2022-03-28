using System.Threading.Tasks;
using Microsoft.Playwright;

namespace FunWithTests.UiTests.Pages
{
    public abstract class PageBase
    {
        public abstract string PagePath { get; }

        public virtual IBrowser Browser { get; private set; }
        public virtual IPage Page { get; private set; }

        public PageBase(IBrowser browser)
        {
            Browser = browser;
        }

        public async Task Navigate()
        {
            Page = await Browser.NewPageAsync();
            await Page.GotoAsync(PagePath);
        }
    }
}