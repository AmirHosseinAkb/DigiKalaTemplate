using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DigiKala.Core.Services.Interfaces;
using DigiKala.Core.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using DigiKala.Core.Security;
using Resources;
using System.Globalization;
using DigiKala.Core.Convertors;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using DigiKala.Data.Entities.User;

namespace DigiKala.Pages.UserPanel
{
    [Authorize]
    public class UserInformationsModel : PageModel
    {
        public UserInformationsModel(IUserService userService)
        {
            _userService = userService;
        }
        private IUserService _userService;


        public UserInformationsViewModel UserInformationsVM { get; set; }
        [BindProperty]
        public UserFullNameViewModel UserFullNameVM { get; set; }
        [BindProperty]
        public UserEmailViewModel UserEmailVM { get; set; }
        [BindProperty]
        public UserNationalNumberViewMode UserNationalNumberVM { get; set; }
        [BindProperty]
        public UserBirthDateViewModel UserBirthDateVM { get; set; }
        [BindProperty]
        public UserPasswordViewModel UserPasswordVM { get; set; }
        [BindProperty]
        public UserPhoneNumberViewModel UserPhoneNumberVM { get; set; }

        public void OnGet()
        {
            UserInformationsVM = _userService.GetUserInformationsForShow(User.Identity!.Name!);
        }
        //*******************

        public void SignInUserAgain(User user,bool byEmail=false)
        {
            var claims=new List<Claim>();
            if (byEmail)
            {
                List<Claim> list = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Email!),
                    new Claim("AvatarName", user.AvatarName)
                };
                claims.AddRange(list);
            }
            else
            {
                List<Claim> list = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                    new Claim(ClaimTypes.Name,user.PhoneNumber!),
                    new Claim("AvatarName",user.AvatarName)
                };
                claims.AddRange(list);
            }
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties()
            {
                IsPersistent = true
            };

            HttpContext.SignInAsync(principal, properties);
        }
        
        //*******************
        public IActionResult OnPostConfirmUserFullName()
        {
            var firstNameValidation = ModelState.GetFieldValidationState("UserFullNameVm.FirstName");
            var lastNameValidation = ModelState.GetFieldValidationState("UserFullNameVm.LastName");
            if (firstNameValidation == ModelValidationState.Invalid || lastNameValidation==ModelValidationState.Invalid)
            {
                return RedirectToPage();
            }
            _userService.ConfirmUserInformations(User.Identity.Name, UserFullNameVM.FirstName, UserFullNameVM.LastName);
            return Content(UserFullNameVM.FirstName + " " + UserFullNameVM.LastName);
        }
        
        public IActionResult OnPostConfirmUserNationalNumber()
        {
            var validateNationalNumber = ModelState.GetFieldValidationState("UserNationalNumberVM.NationalNumber");
            if (validateNationalNumber == ModelValidationState.Invalid)
            {
                return RedirectToPage();
            }
            _userService.ConfirmUserInformations(User.Identity.Name, "", "", UserNationalNumberVM.NationalNumber);
            return Content(UserNationalNumberVM.NationalNumber);
        }
        public IActionResult OnPostConfirmUserPhoneNumber()
        {
            var validatePhoneNumber = ModelState.GetFieldValidationState("UserPhoneNumberVM.PhoneNumber");
            if (validatePhoneNumber == ModelValidationState.Invalid)
            {
                return RedirectToPage();
            }
            var user=_userService.ConfirmUserInformations(User.Identity.Name, "", "", "", UserPhoneNumberVM.PhoneNumber);
            SignInUserAgain(user);
            return Content(UserPhoneNumberVM.PhoneNumber);
        }

        public IActionResult OnPostConfirmUserEmail()
        {
            var validateEmail = ModelState.GetFieldValidationState("UserEmailVm.Email");
            if (validateEmail == ModelValidationState.Invalid)
            {
                return RedirectToPage();
            }
            if (User.Identity!.Name == UserEmailVM.Email.ToLower())
            {
                return BadRequest(ErrorMessages.EnterOtherEmail);
            }
            if (_userService.IsExistUserByEmail(UserEmailVM.Email))
            {
                return BadRequest(ErrorMessages.EmailExists);
            }
            var user=_userService.ConfirmUserInformations(User.Identity!.Name!, "", "", "", "", UserEmailVM.Email);
            SignInUserAgain(user, true);
            return Content(UserEmailVM.Email);
        }
        public IActionResult OnPostConfirmUserBirthDate()
        {
            try
            {
                PersianCalendar persianCalendar = new PersianCalendar();
                var date = persianCalendar.ToDateTime(int.Parse(UserBirthDateVM.BirthYear), int.Parse(UserBirthDateVM.BirthMonth), int.Parse(UserBirthDateVM.BirthDay), 0, 0, 0, 0);
            }
            catch(Exception error)
            {
                return BadRequest(ErrorMessages.EnterDateCorrectly);
            }
            var birthDate = new DateTime(int.Parse(UserBirthDateVM.BirthYear), int.Parse(UserBirthDateVM.BirthMonth), int.Parse(UserBirthDateVM.BirthDay),new PersianCalendar()).ToString();
            _userService.ConfirmUserInformations(User.Identity.Name, "", "", "", "", "", birthDate);
            return Content(DateTime.Parse(birthDate).ToShamsi());
        }
        public IActionResult OnPostConfirmUserPassword()
        {
            var validateCurrentPassword = ModelState.GetFieldValidationState("UserPasswordVM.CurrentPassword");
            var validateNewPassword = ModelState.GetFieldValidationState("UserPasswordVM.NewPassword");
            var validateRepeatPassword = ModelState.GetFieldValidationState("UserPasswordVM.RepeatNewergfdPassword");
            
            if (validateCurrentPassword == ModelValidationState.Invalid
                || validateNewPassword == ModelValidationState.Invalid
                || validateRepeatPassword == ModelValidationState.Invalid)
            {
                return RedirectToPage();
            }
            var user = _userService.GetUserByEmail(User.Identity.Name);
            if (user.Password != PasswordHasher.HashPasswordMD5(UserPasswordVM.CurrentPassword))
            {
                return BadRequest(ErrorMessages.EnterCurrentPasswordCorrectly);
            }
            if (UserPasswordVM.NewPassword != UserPasswordVM.RepeatNewPassword)
            {
                return BadRequest(ErrorMessages.EnterPasswordRepeatCorrectly);
            }
            _userService.ChangeUserPassword(User.Identity.Name, UserPasswordVM.NewPassword);
            return Content(ConfirmationMessages.PasswordChangedSuccessfully);
        }
    }
}
