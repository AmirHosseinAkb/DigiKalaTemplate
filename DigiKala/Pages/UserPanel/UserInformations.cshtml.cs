using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DigiKala.Core.Services.Interfaces;
using DigiKala.Core.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace DigiKala.Pages.UserPanel
{
    [Authorize]
    public class UserInformationsModel : PageModel
    {
        public UserInformationsModel(IUserService userService)
        {
            _userService=userService;
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

        public IActionResult OnPostConfirmUserInformations()
        {
            
            _userService.ConfirmUserInformations(User.Identity.Name, UserFullNameVM.FirstName, UserFullNameVM.LastName
                ,UserNationalNumberVM.NationalNumber,UserPhoneNumberVM.PhoneNumber,UserEmailVM.Email,UserBirthDateVM.BirthDay
                ,UserPasswordVM.NewPassword);
            
            return RedirectToPage();
        }
    }
}
