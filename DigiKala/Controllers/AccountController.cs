using DigiKala.Core.Convertors;
using DigiKala.Core.Generators;
using DigiKala.Core.Security;
using DigiKala.Core.Senders;
using DigiKala.Core.Services.Interfaces;
using DigiKala.Core.ViewModels.User;
using DigiKala.Data.Entities.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigiKala.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(IUserService userService, IViewRenderService viewRenderService)
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
            if (registerAndLoginVM.EmailOrPhoneNumber.IsEmail())
            {
                var user = _userService.GetUserByEmail(registerAndLoginVM.EmailOrPhoneNumber);
                TempData["EmailAddressCheck"] = registerAndLoginVM.EmailOrPhoneNumber;
                TempData["EmailAddress"] = registerAndLoginVM.EmailOrPhoneNumber;
                if (user != null)
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
                if (registerAndLoginVM.EmailOrPhoneNumber.Any(x => char.IsLetter(x)) || registerAndLoginVM.EmailOrPhoneNumber.Length != 10)
                {
                    ModelState.AddModelError("EmailOrPhone", "لطفا شماره تلفن را به صورت صحیح وارد کنید");
                    return View(registerAndLoginVM);
                }

            }
            return Redirect("");
        }

        [Route("Login")]

        public IActionResult Login()
        {
            if (TempData["EmailAddressCheck"] == null)
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
            var user = _userService.GetUserByEmail(email!);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Name,user.Email!),
                new Claim("AvatarName",user.AvatarName)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties()
            {
                IsPersistent = true
            };
            HttpContext.SignInAsync(principal, properties);
            return Redirect("/");
        }

        [Route("Register")]
        public IActionResult Register()
        {
            if (TempData["EmailAddressCheck"] == null)
                return Redirect("/RegisterAndLogin");

            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid)
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
                MessageCode = RandomNumberGenerator.RandomIntegerGenerator(10000, 99999).ToString(),
                ActivationCode = NameGenerator.GenerateUniqName(),
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

        [Route("ForgetPassword")]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public IActionResult ForgetPassword(ForgetPasswordViewModel forgetPasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return View(forgetPasswordVM);
            }
            if (forgetPasswordVM.EmailOrPhoneNumber.IsEmail())
            {
                var user = _userService.GetUserByEmail(forgetPasswordVM.EmailOrPhoneNumber);
                if (user != null)
                {
                    var body = _viewRenderService.RenderToStringAsync("",);
                }
                else
                {
                    ModelState.AddModelError("EmailOrPhoneNumber", "کاربری با ایمیل وارد شده موجود نمی باشد");
                    return View(forgetPasswordVM);
                }

            }
            else
            {
                if (forgetPasswordVM.EmailOrPhoneNumber.Any(x => char.IsLetter(x)) || forgetPasswordVM.EmailOrPhoneNumber.Length != 10)
                {
                    ModelState.AddModelError("EmailOrPhoneNumber", "لطفا شماره تلفن را به صورت صحیح وارد کنید");
                    return View(forgetPasswordVM);
                }
            }
            return null;
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}
