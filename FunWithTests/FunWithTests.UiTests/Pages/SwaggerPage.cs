using System.Threading.Tasks;
using Microsoft.Playwright;

namespace FunWithTests.UiTests.Pages
{
    public class SwaggerPage : PageBase
    {

        public override string PagePath => "https://localhost:6001/swagger/index.html";

        public SwaggerPage(IBrowser browser)
            : base(browser) { }

        public async Task ClickAuthorizeButton()
        {
            await Page.ClickAsync(".btn.authorize");
        }
        public async Task<string> GetClientIdText()
        {
            return await Page.InputValueAsync("#client_id");
        }
    }
}
