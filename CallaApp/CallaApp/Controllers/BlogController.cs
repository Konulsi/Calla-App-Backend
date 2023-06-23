using CallaApp.Models;
using CallaApp.Services;
using CallaApp.Services.Interfaces;
using CallaApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly ITagService _tagService;
        private readonly ICategoryService _categoryService;
        private readonly ILayoutService _layoutService;

        public BlogController(ILayoutService layoutService,
                              ICategoryService categoryService,
                              ITagService tagService)
        {
            _layoutService = layoutService;
            _categoryService = categoryService;
            _tagService = tagService;
        }
        public async Task<IActionResult>  Index()
        {
            List<Tag> tags = await _tagService.GetAllAsync();
            List<HeaderBackground> headerBackgrounds = _layoutService.GetAllAsync();
            List<Category> categories = await _categoryService.GetAllAsync();

            BlogVM model = new()
            {
                Tags = tags,
                Categories = categories,
                HeaderBackgrounds = headerBackgrounds,
            };

            return View(model);
        }
    }
}
