using Microsoft.AspNetCore.Mvc;

namespace DigiKala.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
