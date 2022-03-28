using System.Threading.Tasks;
using FunWithTests.UiTests.Pages;
using Microsoft.Playwright;
using Xunit;

namespace FunWithTests.UiTests
{
    public class SwaggerTest
    {
        [Fact]
        public async Task Authorize_modal_should_have_correct_client()
        {
            // Arrange
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions()
            {
                // Headless = false,
                // SlowMo = (float)TimeSpan.FromSeconds(2).TotalMilliseconds,
            });
            var page = new SwaggerPage(browser);

            // Act
            await page.Navigate();
            await page.ClickAuthorizeButton();
            var text = await page.GetClientIdText();

            // Assert
            Assert.Equal(expected: "swagger", actual: text);
        }
    }
}
