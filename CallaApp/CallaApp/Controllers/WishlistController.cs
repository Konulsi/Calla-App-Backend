using CallaApp.Models;
using CallaApp.Services.Interfaces;
using CallaApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace CallaApp.Controllers
{
    public class WishlistController : Controller
    {
        private readonly ILayoutService _layoutService;
        public WishlistController(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }
        public async Task<IActionResult> Index()
        {
            List<HeaderBackground> headerBackgrounds = _layoutService.GetAllAsync();

            WishlistVM model = new()
            {
                HeaderBackgrounds = headerBackgrounds,
            };
            return View(model);
        }
    }
}
