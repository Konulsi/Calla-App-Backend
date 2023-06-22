using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ILayoutService _layoutService;

        public SettingController(IWebHostEnvironment env,
                        ILayoutService layoutService,
                        AppDbContext context)
        {
            _env = env;
            _layoutService = layoutService;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Settings> settings = _layoutService.GetSettingDatas();
            return View(settings);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            Settings dbSetting = await _layoutService.GetById(id);

            Settings model = new()
            {
                Value = dbSetting.Value,
            };

            return View(model);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Settings updatedSetting)
        {
            try
            {
                if (id == null) return BadRequest();
                Settings dbSetting = await _layoutService.GetById(id);
                if (dbSetting is null) return NotFound();

                Settings model = new()
                {
                    Value = dbSetting.Value,
                };

                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                dbSetting.Value = updatedSetting.Value;
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
