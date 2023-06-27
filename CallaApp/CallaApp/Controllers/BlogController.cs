using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services;
using CallaApp.Services.Interfaces;
using CallaApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ITagService _tagService;
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly IMiniImageService _miniImageService;
        public BlogController(AppDbContext context,
                              ICategoryService categoryService,
                              ITagService tagService,
                              IMiniImageService miniImageService,
                              IBlogService blogService)
        {
            _categoryService = categoryService;
            _tagService = tagService;
            _miniImageService = miniImageService;
            _context = context;
            _blogService = blogService;
        }
        public async Task<IActionResult>  Index()
        {
            List<Blog> blogs = await _blogService.GetAllAsync();
            List<Tag> tags = await _tagService.GetAllAsync();
            List<Category> categories = await _categoryService.GetAllAsync();
            List<MiniImage> miniImages = await _miniImageService.GetAllAsync();
            Dictionary<string, string> headerBackgrounds = _context.HeaderBackgrounds.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);


            BlogVM model = new()
            {
                Tags = tags,
                Categories = categories,
                HeaderBackgrounds = headerBackgrounds,
                MiniImages = miniImages,
                Blogs = blogs
            };

            return View(model);
        }
    }
}
