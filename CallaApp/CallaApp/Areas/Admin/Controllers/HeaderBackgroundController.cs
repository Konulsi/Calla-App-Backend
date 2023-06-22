using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services;
using CallaApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HeaderBackgroundController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILayoutService _layoutService;
        public HeaderBackgroundController(AppDbContext context,
                                      ILayoutService layoutService)
        {
            _context = context;
            _layoutService = layoutService;
        }
        public IActionResult Index()
        {
            List<HeaderBackground> headerBackgrounds =  _layoutService.GetAllAsync();
            return View(headerBackgrounds);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            HeaderBackground dbHeaderBackground = await _layoutService.GetByIdAsync(id);

            HeaderBackground model = new()
            {
                Value = dbHeaderBackground.Value,
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, HeaderBackground updatedHeaderBackground)
        {
            try
            {
                if (id == null) return BadRequest();
                HeaderBackground dbHeaderBackground = await _layoutService.GetByIdAsync(id);
                if (dbHeaderBackground is null) return NotFound();

                HeaderBackground model = new()
                {
                    Value = dbHeaderBackground.Value,
                };

                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                dbHeaderBackground.Value = updatedHeaderBackground.Value;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                @ViewBag.error = ex.Message;
                return View();
            }
        }


    }
}
