using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services;
using CallaApp.Services.Interfaces;
using CallaApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ITagService _tagService;
        private readonly ISizeService _sizeService;
        private readonly IColorService _colorService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;


        public ShopController(ITagService tagService,
                              ISizeService sizeService,
                              ICategoryService categoryService,
                              IColorService colorService,
                              IBrandService brandService,
                              AppDbContext context)
        {
            _tagService = tagService;   
            _sizeService = sizeService;
            _categoryService = categoryService;
            _colorService = colorService;
            _brandService = brandService;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Tag> tags = await _tagService.GetAllAsync();
            List<Size> sizes = await _sizeService.GetAllAsync();
            List<Color> colors = await _colorService.GetAllAsync();
            List<Brand> brands = await _brandService.GetAllAsync();
            List<Category> categories = await _categoryService.GetAllAsync();
            Dictionary<string, string> headerBackgrounds = _context.HeaderBackgrounds.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);

            ShopVM model = new()
            {
                Tags = tags,
                Sizes = sizes,
                Categories = categories,
                Colors = colors,
                Brands = brands,
                HeaderBackgrounds = headerBackgrounds,
            };

            return View(model);
        }
    }
}
