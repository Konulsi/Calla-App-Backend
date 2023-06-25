using CallaApp.Models;
using CallaApp.Services.Interfaces;
using CallaApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ILayoutService _layoutService;
        public CartController(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }
        public async Task<IActionResult> Index()
        {
            List<HeaderBackground> headerBackgrounds = _layoutService.GetAllAsync();

            CartVM model = new()
            {
                HeaderBackgrounds = headerBackgrounds,
            };
            return View(model);
        }
    }
}
