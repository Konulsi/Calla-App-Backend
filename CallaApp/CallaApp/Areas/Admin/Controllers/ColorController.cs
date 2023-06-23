using CallaApp.Areas.Admin.ViewModels.Color;
using CallaApp.Areas.Admin.ViewModels.Size;
using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services;
using CallaApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IColorService _colorService;
        public ColorController(AppDbContext context,
                                IWebHostEnvironment env,
                                IColorService colorService)
        {
            _context = context;
            _env = env;
            _colorService = colorService;
        }
        public async Task<IActionResult> Index()
        {
            List<Color> colors = await _colorService.GetAllAsync();
            return View(colors);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ColorCreateVM color)
        {
            try
            {
                Color newColor = new()
                {
                    Name = color.Name
                };

                await _context.Colors.AddAsync(newColor);
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
            Color dbColor = await _colorService.GetByIdAsync(id);
            if (dbColor is null) return NotFound();

            ColorUpdateVM model = new()
            {
                Name = dbColor.Name
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ColorUpdateVM colorUpdate)
        {
            try
            {
                if (id == null) return BadRequest();
                Color dbColor = await _colorService.GetByIdAsync(id);
                if (dbColor is null) return NotFound();

                ColorUpdateVM model = new()
                {
                    Name = dbColor.Name
                };

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                dbColor.Name = colorUpdate.Name;
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
                Color dbColor = await _colorService.GetByIdAsync(id);
                if (dbColor is null) return NotFound();

                _context.Colors.Remove(dbColor);
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
