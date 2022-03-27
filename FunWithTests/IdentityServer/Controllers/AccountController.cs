using System.Threading.Tasks;
using IdentityServer.Models;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Services;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEventService _eventService;
        private readonly TestUserStore _testUserStore;

        public AccountController(IEventService eventService, TestUserStore testUserStore)
        {
            _eventService = eventService;
            _testUserStore = testUserStore;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var loginModel = new LoginModel
            {
                ReturnUrl = returnUrl,
            };

            return View("Login", loginModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            if (!_testUserStore.ValidateCredentials(loginModel.UserName, loginModel.Password))
            {
                return View();
            }

            var user = _testUserStore.FindByUsername(loginModel.UserName);

            var isuser = new IdentityServerUser(user.SubjectId)
            {
                DisplayName = user.Username
            };
            await HttpContext.SignInAsync(isuser);

            await _eventService.RaiseAsync(new UserLoginSuccessEvent(username: user.Username, subjectId: user.Username, name: user.Username));

            return Redirect(loginModel.ReturnUrl);
        }
    }
}
