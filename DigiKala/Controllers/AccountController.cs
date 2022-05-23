using DigiKala.Core.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using DigiKala.Core.Services.Interfaces;
using DigiKala.Data.Entities;
using DigiKala.Data.Entities.User;
using DigiKala.Core.Convertors;
using DigiKala.Core.Security;
using DigiKala.Core.Generators;
using DigiKala.Core.Senders;

namespace DigiKala.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(IUserService userService,IViewRenderService viewRenderService)
        {
            _userService = userService;
            _viewRenderService = viewRenderService;
        }
        private IUserService _userService;
        private IViewRenderService _viewRenderService;

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
                    return Redirect("/Login");
                }
                else
                {
                    TempData["EmailAddress"] = registerAndLoginVM.EmailOrPhoneNumber;
                    return Redirect("/Register");
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

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel registerVM)
        {
            if(!ModelState.IsValid)
            {
                return View(registerVM);
            }
            var email = TempData["EmailAddress"]?.ToString();
            var user = new User()
            {
                Email = EmailConvertor.FixEmail(email),
                Password = PasswordHasher.HahsPasswordMD5(registerVM.Password),
                AvatarName = "Default.png",
                IsActive = false,
                MessageCode = RandomNumberGenerator.RandomIntegerGenerator(100000,99999).ToString(),
                ActivationCode=NameGenerator.GenerateUniqName(),
                RegisterDate = DateTime.Now
            };
            //Add User
            _userService.AddUser(user);

            //Send Email
            var body = _viewRenderService.RenderToStringAsync("/Account/ActivationEmail", user);
            SendEmail.Send(user.Email, "فعالسازی حساب کاربری", body);

            return Redirect("/Account/SuccessRegister");
        }

        public IActionResult ActiveAccount(string activeCode)
        {
            ViewBag.isActivated = _userService.ActiveUserAccount(activeCode);
            return View();
        }
    }
}
