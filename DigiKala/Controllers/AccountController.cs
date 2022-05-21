using DigiKala.Core.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace DigiKala.Controllers
{
    public class AccountController : Controller
    {
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
                
            }
            else
            {

            }
            return Redirect("");
        }
    }
}
