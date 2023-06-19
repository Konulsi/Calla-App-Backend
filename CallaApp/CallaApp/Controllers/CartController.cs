using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
