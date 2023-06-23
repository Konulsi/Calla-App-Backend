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
        public ShopController(ITagService tagService)
        {
            _tagService = tagService;    
        }
        public async Task<IActionResult> Index()
        {
            List<Tag> tags = await _tagService.GetAllAsync();

            ShopVM model = new()
            {
                Tags = tags,
            };

            return View(model);
        }
    }
}
