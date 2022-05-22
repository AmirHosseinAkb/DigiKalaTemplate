using DigiKala.Core.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using DigiKala.Core.Services.Interfaces;

namespace DigiKala.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        private IUserService _userService;

        [Route("RegisterAndLogin")]
        public IActionResult RegisterAndLogin()
        {
            return View();
        }

        [Route("RegisterAndLogin")]
        [HttpPost]
        public IActionResult RegisterAndLogin(RegisterAndLoginViewModel registerAndLoginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerAndLoginVM);
            }
            var emailAddress = new System.Net.Mail.MailAddress(registerAndLoginVM.EmailOrPhoneNumber);
            if (registerAndLoginVM.EmailOrPhoneNumber == emailAddress.Address)
            {
                if (_userService.IsExistUserByEmail(registerAndLoginVM.EmailOrPhoneNumber))
                {
                    return Redirect("/Login/"+emailAddress);
                }
                else
                {
                    return Redirect("/Register/"+emailAddress);
                }
            }
            else
            {

            }
            return Redirect("");
        }

        [Route("Login/{emailAddress}")]

        public IActionResult Login(string emailAddress)
        {
            return View();
        }

        [Route("Register/{emailAddress}")]
        public IActionResult Register(string emailAddress)
        {
            return View();
        }
    }
}
