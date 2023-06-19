using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Controllers
{
    public class WishlistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
