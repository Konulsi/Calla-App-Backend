using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
