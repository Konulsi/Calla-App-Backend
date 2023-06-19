using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
