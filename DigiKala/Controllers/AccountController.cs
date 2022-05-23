using DigiKala.Core.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using DigiKala.Core.Services.Interfaces;
using DigiKala.Data.Entities;
using DigiKala.Data.Entities.User;
using DigiKala.Core.Convertors;
using DigiKala.Core.Security;
using DigiKala.Core.Generators;
using DigiKala.Core.Senders;
using System.Security.Claims;

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
                var user = _userService.GetUserByEmail(registerAndLoginVM.EmailOrPhoneNumber);
                TempData["EmailAddress"] = registerAndLoginVM.EmailOrPhoneNumber;
                if (user!=null)
                {
                    if (!user.IsActive)
                    {
                        ModelState.AddModelError("EmailOrPhoneNumber", "حساب کاربری شما غیرفعال می باشد لطفا ایمیلتان را بررسی کنید یا از طریق شماره تلفن وارد شوید.");
                        return View(registerAndLoginVM);
                    }
                    return Redirect("/Login");
                }
                else
                {
                    return Redirect("/Register");
                }
            }
            else
            {
                
            }
            return Redirect("");
        }

        [Route("Login")]

        public IActionResult Login()
        {
            if (TempData["EmailAddress"] == null) 
                return Redirect("/RegisterAndLogin");

            return View();
        }
        [Route("Login")]
        [HttpPost]
        public IActionResult Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            var email = TempData["EmailAddress"]?.ToString();
            var user=_userService.GetUserByEmail(email);    
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserId.ToString())
            };
            return null;
        }

        [Route("Register")]
        public IActionResult Register()
        {
            if (TempData["EmailAddress"] == null)
                return Redirect("/RegisterAndLogin");

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
                MessageCode = RandomNumberGenerator.RandomIntegerGenerator(10000,99999).ToString(),
                ActivationCode=NameGenerator.GenerateUniqName(),
                RegisterDate = DateTime.Now
            };
            //Add User
            _userService.AddUser(user);

            //Send Email
            var body = _viewRenderService.RenderToStringAsync("Account/ActivationEmail", user);
            SendEmail.Send(user.Email, "فعالسازی حساب کاربری", body);

            return View("SuccessRegister");
        }

        [Route("ActiveAccount/{activeCode}")]
        public IActionResult ActiveAccount(string activeCode)
        {
            ViewBag.isActivated = _userService.ActiveUserAccount(activeCode);
            return View();
        }
    }
}
