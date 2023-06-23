using CallaApp.Models;
using CallaApp.Services;
using CallaApp.Services.Interfaces;
using CallaApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Controllers
{
    public class ShopController : Controller
    {
        private readonly ITagService _tagService;
        private readonly ISizeService _sizeService;
        private readonly IColorService _colorService;
        private readonly ICategoryService _categoryService;


        public ShopController(ITagService tagService,
                              ISizeService sizeService,
                              ICategoryService categoryService,
                              IColorService colorService)
        {
            _tagService = tagService;   
            _sizeService = sizeService;
            _categoryService = categoryService;
            _colorService = colorService;
        }
        public async Task<IActionResult> Index()
        {
            List<Tag> tags = await _tagService.GetAllAsync();
            List<Size> sizes = await _sizeService.GetAllAsync();
            List<Color> colors = await _colorService.GetAllAsync();
            List<Category> categories = await _categoryService.GetAllAsync();


            ShopVM model = new()
            {
                Tags = tags,
                Sizes = sizes,
                Categories = categories,
                Colors = colors
            };

            return View(model);
        }
    }
}
