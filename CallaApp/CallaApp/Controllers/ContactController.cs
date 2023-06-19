using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
