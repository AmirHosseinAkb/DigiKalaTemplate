using DigiKala.Core.Services.Interfaces;
using DigiKala.Core.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiKala.Pages.Admin.Users
{
    public class DeletedUsersListModel : PageModel
    {
        public DeletedUsersListModel(IUserService userService)
        {
            _userService = userService;
        }
        private IUserService _userService;


        public Tuple<List<UsersInformationsForShowInAdminViewModel>, int, int, int> UsersInformationsVm { get; set; }
        public void OnGet(int pageId = 1, string fullName = "", string email = "", string phoneNumber = "", int take = 20)
        {
            if (take % 20 != 0)
                take = 20;
            ViewData["Take"] = take;
            UsersInformationsVm = _userService.GetDeletedUsersInformationsForShowInAdmin(pageId, fullName, email, phoneNumber, take);
        }
        public IActionResult OnPostReturnUser(int userId)
        {
            _userService.ReturnUser(userId);
            return RedirectToPage("Index");
        }
    }
}
