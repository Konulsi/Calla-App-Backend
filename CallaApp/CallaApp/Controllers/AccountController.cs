using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
