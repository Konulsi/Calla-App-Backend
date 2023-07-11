using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using CallaApp.ViewModels.Account;
using CallaApp.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CallaApp.Controllers
{
    //main project
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISliderService _sliderService;
        private readonly IBannerService _bannerService;
        private readonly IDecorService _decorService;
        private readonly IBrandService _brandService;
        private readonly ITeamService _teamService;
        private readonly IBlogService _blogService;
        private readonly IProductService _productService;
        private readonly IAdvertisingService _advertisingService;
        private readonly ISubscribeService _subscribeService;
        public HomeController(ISliderService sliderService,
                              IBannerService bannerService,
                              IAdvertisingService advertisingService,
                              IDecorService decorService,
                              IBrandService brandService,
                              ITeamService teamService,
                              AppDbContext context,
                              IBlogService blogService,
                              IProductService productService,
                              ISubscribeService subscribeService)
        {
            _sliderService = sliderService;
            _bannerService = bannerService;
            _advertisingService = advertisingService;
            _decorService = decorService;
            _brandService = brandService;
            _teamService = teamService;
            _context = context;
            _blogService = blogService;
            _productService = productService;
            _subscribeService = subscribeService;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _sliderService.GetAllAsync();
            List<Product> products = await _productService.GetAllAsync();
            List<Banner> banners = await _bannerService.GetAllAsync();
            List<Decor> decors = await _decorService.GetAllAsync();
            List<Brand> brands = await _brandService.GetAllAsync();
            List<Team> teams = await _teamService.GetAllAsync();
            List<Blog> blogs = await _blogService.GetAllAsync();
            List<Advertising> advertisings = await _advertisingService.GetAllAsync();
            Dictionary<string, string> headerBackgrounds = _context.HeaderBackgrounds.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);

            HomeVM model = new()
            {
                Sliders = sliders,
                HeaderBackgrounds = headerBackgrounds,
                Banners = banners,
                Advertisings = advertisings,
                Decors = decors,
                Brands = brands,
                Teams = teams,
                Blogs = blogs,
                Products = products,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostSubscribe(SubscribeVM model)
        {
            try
            {
                if (!ModelState.IsValid) return RedirectToAction("Index", model);
                var existEmail = await _context.Subscribes.FirstOrDefaultAsync(m=>m.Email == model.Email);

                if (existEmail != null)
                {
                    ModelState.AddModelError("Email", "Email already exist");
                    return RedirectToAction("Index");
                }
                Subscribe subscribe = new()
                {
                    Email = model.Email,
                };

                await _context.Subscribes.AddAsync(subscribe);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Index");
            }
        }

    }
}
