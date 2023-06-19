using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using CallaApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace CallaApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISliderService _sliderService;
        private readonly IHeaderBackgroundService _headerBackgroundService;


        public HomeController(AppDbContext context,
                              ISliderService sliderService,
                              IHeaderBackgroundService headerBackgroundService)
        {
            _context = context;
            _sliderService = sliderService;
            _headerBackgroundService = headerBackgroundService;

        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _sliderService.GetAll();
            List<HeaderBackground> headerBackgrounds =  _headerBackgroundService.GetHeaderBackgroundsAsync();

            HomeVM model = new()
            {
                Sliders = sliders,
                HeaderBackgrounds = headerBackgrounds
            };

            return View(model);
        }
    }
}
