using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
