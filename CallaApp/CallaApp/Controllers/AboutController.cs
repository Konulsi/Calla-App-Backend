using CallaApp.Models;
using CallaApp.Services.Interfaces;
using CallaApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Controllers
{
    public class AboutController : Controller
    {
        private readonly ILayoutService _layoutService;
        private readonly IAboutService _aboutService;
        public AboutController(ILayoutService layoutService,
                              IAboutService aboutService)
        {
            _layoutService = layoutService;
            _aboutService = aboutService;
        }
        public async Task<IActionResult> Index()
        {
            List<HeaderBackground> headerBackgrounds = _layoutService.GetAllAsync();
            List<About> abouts = await _aboutService.GetAllAsync();

            AboutVM model = new()
            {
                HeaderBackgrounds = headerBackgrounds,
                Abouts = abouts
            };

            return View(model);
        }
    }
}
