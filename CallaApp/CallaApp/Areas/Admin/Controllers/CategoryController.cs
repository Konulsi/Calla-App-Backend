using CallaApp.Areas.Admin.ViewModels.Category;
using CallaApp.Areas.Admin.ViewModels.Size;
using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services;
using CallaApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ICategoryService _categoryService;
        public CategoryController(AppDbContext context,
                                IWebHostEnvironment env,
                                ICategoryService categoryService)
        {
            _context = context;
            _env = env;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _categoryService.GetAllAsync();
            return View(categories);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM category)
        {
            try
            {
                Category newCategory = new()
                {
                    Name = category.Name
                };

                await _context.Categories.AddAsync(newCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                throw;
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();
            Category dbCategory = await _categoryService.GetByIdAsync(id);
            if (dbCategory is null) return NotFound();

            CategoryUpdateVM model = new()
            {
                Name = dbCategory.Name
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CategoryUpdateVM categoryUpdate)
        {
            try
            {
                if (id == null) return BadRequest();
                Category dbCategory = await _categoryService.GetByIdAsync(id);
                if (dbCategory is null) return NotFound();

                CategoryUpdateVM model = new()
                {
                    Name = dbCategory.Name
                };
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                dbCategory.Name = categoryUpdate.Name;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                @ViewBag.error = ex.Message;
                return View();
            }
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Category dbCategory = await _categoryService.GetByIdAsync(id);
                if (dbCategory is null) return NotFound();

                _context.Categories.Remove(dbCategory);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }
    }
}
