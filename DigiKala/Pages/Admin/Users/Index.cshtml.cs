using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DigiKala.Core.Services.Interfaces;
using DigiKala.Core.ViewModels.User;

namespace DigiKala.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }
        private IUserService _userService;
        
        public Tuple<List<UsersInformationsForShowInAdminViewModel>,int,int,int> UsersInformationsVm { get; set; }
        
        public void OnGet(int pageId = 1, string fullName = "", string email = "", string phoneNumber = "", int take = 10)
        {
            UsersInformationsVm = _userService.GetUsersInformationsForShowInAdmin(pageId, fullName, email, phoneNumber, take);
        }
    }
}
