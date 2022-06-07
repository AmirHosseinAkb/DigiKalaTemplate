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
using Resources;

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
        public IActionResult RegisterAndLogin(bool emailChanged=false)
        {
            ViewBag.IsEmailChanged = emailChanged;
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
                HttpContext.Session.SetString("EmailAddress", registerAndLoginVM.EmailOrPhoneNumber);
                if (user != null)
                {
                    if (!user.IsActive)
                    {
                        ModelState.AddModelError("EmailOrPhoneNumber", ErrorMessages.NoActiveUserAccount);
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
                    ModelState.AddModelError("EmailOrPhoneNumber", ErrorMessages.EnterPhoneNumberCorrectly);
                    return View(registerAndLoginVM);
                }
                var user = _userService.GetUserByPhoneNumber(registerAndLoginVM.EmailOrPhoneNumber);
                if (user == null)
                {
                    var verificationCode = RandomNumberGenerator.GenerateRendomInteger(10000, 99999);
                    MessageSender.SendMessage(registerAndLoginVM.EmailOrPhoneNumber
                        , DataDictionaries.AuthorizationMessageText + " " + verificationCode);
                    HttpContext.Session.SetInt32("VerificationCode", verificationCode);
                    HttpContext.Session.SetString("PhoneNumber", registerAndLoginVM.EmailOrPhoneNumber);
                    return Redirect("");
                }
            }
            return Redirect("Verification");
        }

        [Route("Verification")]
        public IActionResult Verification()
        {
            return View();
        }
        
        [HttpPost]
        [Route("Verification")]
        public IActionResult Verification()
        {
            var phoneNumber = HttpContext.Session.GetString("PhoneNumber");
            var user = _userService.GetUserByPhoneNumber(phoneNumber);
            ViewBag.IsExistUser = user;
            ViewBag.PhoneNumber = phoneNumber;
            return View();
        }

        [Route("Login")]
        public IActionResult Login(bool emailChanged = false)
        {
            if (HttpContext.Session.GetString("EmailAddress") == null)
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
            var email = HttpContext.Session.GetString("EmailAddress");
            var user = _userService.IsExistUserForLogin(email!, loginVM.Password);
            if (user != null)
            {
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
                return Redirect("/UserPanel");
            }
            else
            {
                ModelState.AddModelError("Password", ErrorMessages.EnterPasswordCorrectly);
                return View(loginVM);
            }

        }

        [Route("Register")]
        public IActionResult Register()
        {
            if (HttpContext.Session.GetString("EmailAddress") == null)
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
            var email = HttpContext.Session.GetString("EmailAddress");
            var user = new User()
            {
                Email = EmailConvertor.FixEmail(email),
                Password = PasswordHasher.HashPasswordMD5(registerVM.Password),
                AvatarName = "Default.png",
                IsActive = false,
                MessageCode = RandomNumberGenerator.GenerateRendomInteger(10000, 99999).ToString(),
                ActivationCode = NameGenerator.GenerateUniqName(),
                RegisterDate = DateTime.Now
            };
            //Add User
            _userService.AddUser(user);

            //Send Email
            var body = _viewRenderService.RenderToStringAsync("Account/ActivationEmail", user);
            SendEmail.Send(user.Email, DataDictionaries.ActiveAccount, body);

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
                    var body = _viewRenderService.RenderToStringAsync("Account/ResetPasswordEmail", user);
                    SendEmail.Send(user.Email, DataDictionaries.ResetPassword, body);
                }
                else
                {
                    ModelState.AddModelError("EmailOrPhoneNumber", ErrorMessages.NoUserWithEnteredEmail);
                    return View(forgetPasswordVM);
                }

            }
            else
            {
                if (forgetPasswordVM.EmailOrPhoneNumber.Any(x => char.IsLetter(x)) || forgetPasswordVM.EmailOrPhoneNumber.Length != 10)
                {
                    ModelState.AddModelError("EmailOrPhoneNumber", ErrorMessages.EnterPhoneNumberCorrectly);
                    return View(forgetPasswordVM);
                }
                var user = _userService.GetUserByPhoneNumber(forgetPasswordVM.EmailOrPhoneNumber);
                if (user != null)
                {
                    var body = _viewRenderService.RenderToStringAsync("Account/ResetPasswordEmail", user);
                    SendEmail.Send(user.Email, DataDictionaries.ResetPassword, body);
                }
                else
                {
                    ModelState.AddModelError("EmailOrPhoneNumber", ErrorMessages.NoUserWithEnteredEmail);
                    return View(forgetPasswordVM);
                }
            }
            ViewBag.EmailSent = true;
            return View();
        }

        [Route("ResetPassword/{activationCode?}")]
        public IActionResult ResetPassword(string activationCode)
        {
            if (!_userService.IsExistUserByActivationCode(activationCode))
            {
                return View("Errors/NotFoundError");
            }
            TempData["ActivationCode"] = activationCode;
            return View();
        }

        [HttpPost]
        [Route("ResetPassword/{activationCode?}")]
        public IActionResult ResetPassword(ResetPasswordViewModel resetPasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPasswordVM);
            }
            var activationCode = TempData["ActivationCode"]!.ToString();
            ViewBag.PasswordReseted = _userService.ResetUserPassword(activationCode, resetPasswordVM.NewPassword);
            return View("SuccessResetPassword");
        }


        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}
