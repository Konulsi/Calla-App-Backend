using CallaApp.Areas.Admin.ViewModels.Size;
using CallaApp.Areas.Admin.ViewModels.Tag;
using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services;
using CallaApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SizeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ISizeService _sizeService;
        public SizeController(AppDbContext context,
                                IWebHostEnvironment env,
                                ISizeService sizeService)
        {
            _context = context;
            _env = env;
            _sizeService = sizeService;
        }
        public async Task<IActionResult> Index()
        {
            List<Size> sizes = await _sizeService.GetAllAsync();
            return View(sizes);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SizeCreateVM size)
        {
            try
            {
                Size newSize = new()
                {
                    Name = size.Name
                };

                await _context.Sizes.AddAsync(newSize);
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
            Size dbSize = await _sizeService.GetByIdAsync(id);
            if (dbSize is null) return NotFound();

            SizeUpdateVM model = new()
            {
                Name = dbSize.Name
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SizeUpdateVM sizeUpdate)
        {
            try
            {
                if (id == null) return BadRequest();
                Size dbSize = await _sizeService.GetByIdAsync(id);
                if (dbSize is null) return NotFound();

                SizeUpdateVM model = new()
                {
                    Name = dbSize.Name
                };
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                dbSize.Name = sizeUpdate.Name;
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
                Size dbSize = await _sizeService.GetByIdAsync(id);
                if (dbSize is null) return NotFound();

                _context.Sizes.Remove(dbSize);
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
