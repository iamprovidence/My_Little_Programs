using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FunWithTests.IntegrationTests.Helpers
{
    public class SwaggerIdentityHelper
    {
        private string _url;
        private string _userName;
        private string _userPassword;

        private static readonly ConcurrentDictionary<string, Task<string>> _cachedTokens = new();

        public string UserName => _userName;

        public string UserPassword => _userPassword;

        public static SwaggerIdentityHelper Create(string url, string userName, string userPassword)
        {
            return new SwaggerIdentityHelper()
            {
                _url = url,
                _userName = userName,
                _userPassword = userPassword,
            };
        }

        public Task<string> GetAcceessToken()
        {
            var key = $"{_userName}~{_userPassword}";

            return _cachedTokens.GetOrAdd(key, async (fact) =>
            {
                using ChromeDriver chromeDriver = CreateChromeDriver();

                await SubmitSwaggerAuthorization(chromeDriver);

                TaskCompletionSource<string> taskCompletionSource = await SubscribeToTokenResponse(chromeDriver);

                await SubmitScadaAuthorization(chromeDriver);

                return await taskCompletionSource.Task;
            });

            ChromeDriver CreateChromeDriver()
            {
                var option = new ChromeOptions();
                option.AddArgument("--headless");

                return new ChromeDriver(option);
            }

            async Task SubmitSwaggerAuthorization(ChromeDriver chromeDriver)
            {
                chromeDriver.Navigate().GoToUrl($"{_url}/swagger");
                await Task.Delay(500);

                IWebElement authorizeButton = chromeDriver.FindElement(By.CssSelector(".btn.authorize"));
                authorizeButton.Click();
                await Task.Delay(500);

                IWebElement authorizeModalButton = chromeDriver.FindElement(By.CssSelector(".btn.modal-btn.auth.authorize"));
                authorizeModalButton.Click();
            }

            async Task<TaskCompletionSource<string>> SubscribeToTokenResponse(ChromeDriver chromeDriver)
            {
                var taskCompletionSource = new TaskCompletionSource<string>();
                await chromeDriver.Manage().Network.StartMonitoring();

                chromeDriver.Manage().Network.NetworkResponseReceived += (s, e) =>
                {
                    if (e.ResponseUrl.Contains("authorize/callback"))
                    {
                        var callbackUrl = e.ResponseHeaders["location"].Replace("#", "?");
                        var query = new Uri(callbackUrl).Query;

                        var parsedQuery = HttpUtility.ParseQueryString(query);

                        string accessToken = parsedQuery["access_token"];

                        taskCompletionSource.SetResult(accessToken);
                    }
                };

                return taskCompletionSource;
            }

            async Task SubmitScadaAuthorization(ChromeDriver chromeDriver)
            {
                chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                await Task.Delay(500);

                IWebElement userNameField = chromeDriver.FindElement(By.CssSelector("#UserName"));
                userNameField.SendKeys(_userName);

                IWebElement passwordField = chromeDriver.FindElement(By.CssSelector("#Password"));
                passwordField.SendKeys(_userPassword);

                IWebElement submitBtn = chromeDriver.FindElement(By.CssSelector(".btn"));
                submitBtn.Click();
            }
        }
    }
}
