using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DigiKala.Core.Services.Interfaces;
using DigiKala.Core.ViewModels.User;
using Resources;
using DigiKala.Core.Generators;

namespace DigiKala.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        public IndexModel(IUserService userService,IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }
        private IUserService _userService;
        private IPermissionService _permissionService;

        [BindProperty]
        public CreateUserViewModel CreateUserVM { get; set; }

        [BindProperty]
        public EditUserViewModel EditUserVM { get; set; }

        public Tuple<List<UsersInformationsForShowInAdminViewModel>,int,int,int> UsersInformationsVm { get; set; }
        
        public void OnGet(int pageId = 1, string fullName = "", string email = "", string phoneNumber = "", int take = 20)
        {
            if (take %20!=0)
                take = 20;
            ViewData["Take"] = take;
            ViewData["Roles"] = _permissionService.GetAllRoles();
            UsersInformationsVm = _userService.GetUsersInformationsForShowInAdmin(pageId, fullName, email, phoneNumber, take);
        }

        public IActionResult OnPostCreateUser(int roleId)
        {
            if (!ModelState.IsValid || roleId==0)
                return RedirectToPage();

            if (!_permissionService.IsExistRoleById(roleId))
                return RedirectToPage();

            _userService.AddUser(CreateUserVM, roleId);
            
            return RedirectToPage();
        }

        public IActionResult OnPostEditUser(int roleId)
        {
            if (!ModelState.IsValid || roleId == 0)
                return RedirectToPage();

            if (!_permissionService.IsExistRoleById(roleId))
                return RedirectToPage();

            return RedirectToPage();
        }

        public IActionResult OnGetIsExistEmailOrPhoneNumber(string email="",string phoneNumber="")
        {
            if(!string.IsNullOrEmpty(email))
                return Content(_userService.IsExistUserByEmail(email).ToString().ToLower());
            if (!string.IsNullOrEmpty(phoneNumber))
                return Content(_userService.IsExistUserByPhoneNumber(phoneNumber).ToString().ToLower());
            return Content("false");
        }
    }
}
