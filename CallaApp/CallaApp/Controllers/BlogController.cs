using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
