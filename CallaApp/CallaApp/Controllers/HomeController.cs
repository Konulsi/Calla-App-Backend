using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using CallaApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILayoutService _layoutService;
        private readonly ISliderService _sliderService;
        private readonly IBannerService _bannerService;
        private readonly IDecorService _decorService;
        private readonly IBrandService _brandService;
        private readonly ITeamService _teamService;
        private readonly IAdvertisingService _advertisingService;


        public HomeController(ISliderService sliderService,
                              ILayoutService layoutService,
                              IBannerService bannerService,
                              IAdvertisingService advertisingService,
                              IDecorService decorService,
                              IBrandService brandService,
                              ITeamService teamService)
        {
            _sliderService = sliderService;
            _bannerService = bannerService;
            _advertisingService = advertisingService;
            _decorService = decorService;
            _layoutService = layoutService;
            _brandService = brandService;
            _teamService = teamService;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _sliderService.GetAllAsync();
            List<Banner> banners = await _bannerService.GetAllAsync();
            List<Decor> decors = await _decorService.GetAllAsync();
            List<Brand> brands = await _brandService.GetAllAsync();
            List<Team> teams = await _teamService.GetAllAsync();
            List<Advertising> advertisings = await _advertisingService.GetAllAsync();
            List<HeaderBackground> headerBackgrounds = _layoutService.GetAllAsync();

            HomeVM model = new()
            {
                Sliders = sliders,
                HeaderBackgrounds = headerBackgrounds,
                Banners = banners,
                Advertisings = advertisings,
                Decors = decors,
                Brands = brands,
                Teams = teams
            };

            return View(model);
        }
    }
}
