using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DigiKala.Core.Services.Interfaces;
using DigiKala.Core.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using DigiKala.Core.Security;

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
        public UserFullNameViewModel UserFullNameVM { get; set; }
        public UserEmailViewModel UserEmailVM { get; set; }
        public UserNationalNumberViewMode UserNationalNumberVM { get; set; }
        public UserBirthDateViewModel UserBirthDateVM { get; set; }
        public UserPasswordViewModel UserPasswordVM { get; set; }
        public UserPhoneNumberViewModel UserPhoneNumberVM { get; set; }

        public void OnGet()
        {
            UserInformationsVM = _userService.GetUserInformationsForShow(User.Identity!.Name!);
        }

        public IActionResult OnGetConfirmUserFullName(string firstName, string lastName)
        {
            if(string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                return RedirectToPage();
            }
            _userService.ConfirmUserInformations(User.Identity.Name, firstName, lastName);
            return Content(firstName + " " + lastName);
        }

        public IActionResult OnPostChangeUserPassword()
        {
            var user = _userService.GetUserByEmail(User.Identity!.Name!);

            if (user.Password != PasswordHasher.HashPasswordMD5(UserPasswordVM.CurrentPassword))
            {
                return RedirectToPage();
            }

            return RedirectToPage();
        }
        public IActionResult OnGetIsCurrentPasswordCorrect(string currentPassword)
        {
            var user = _userService.GetUserByEmail(User.Identity!.Name!);
            var isCorrect = user.Password == PasswordHasher.HashPasswordMD5(currentPassword);
            return Content(isCorrect.ToString().ToLower());
        }
    }
}
