using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using CallaApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace CallaApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISliderService _sliderService;
        private readonly IBannerService _bannerService;
        private readonly IDecorService _decorService;
        private readonly IAdvertisingService _advertisingService;
        private readonly IHeaderBackgroundService _headerBackgroundService;


        public HomeController(AppDbContext context,
                              ISliderService sliderService,
                              IHeaderBackgroundService headerBackgroundService,
                              IBannerService bannerService,
                              IAdvertisingService advertisingService,
                              IDecorService decorService)
        {
            _context = context;
            _sliderService = sliderService;
            _headerBackgroundService = headerBackgroundService;
            _bannerService = bannerService;
            _advertisingService = advertisingService;
            _decorService = decorService;

        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _sliderService.GetAllAsync();
            List<Banner> banners = await _bannerService.GetAllAsync();
            List<Decor> decors = await _decorService.GetAllAsync();
            List<Advertising> advertisings = await _advertisingService.GetAllAsync();
            List<HeaderBackground> headerBackgrounds =  _headerBackgroundService.GetHeaderBackgroundsAsync();

            HomeVM model = new()
            {
                Sliders = sliders,
                HeaderBackgrounds = headerBackgrounds,
                Banners = banners,
                Advertisings = advertisings,
                Decors = decors
            };

            return View(model);
        }
    }
}
