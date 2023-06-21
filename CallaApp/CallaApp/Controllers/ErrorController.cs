using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
