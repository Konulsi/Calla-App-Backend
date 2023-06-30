using CallaApp.Data;
using CallaApp.Helpers;
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
        private readonly IWebHostEnvironment _env;
        private readonly ILayoutService _layoutService;
        public HeaderBackgroundController(AppDbContext context,
                                      ILayoutService layoutService,
                                      IWebHostEnvironment env)
        {
            _context = context;
            _layoutService = layoutService;
            _env = env;
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

                if (dbHeaderBackground.Value.Contains(".png") || dbHeaderBackground.Value.Contains(".jpg") || dbHeaderBackground.Value.Contains(".jpeg"))
                {
                    if (updatedHeaderBackground.BackgroundPhoto is not null)
                    {

                        if (!updatedHeaderBackground.BackgroundPhoto.CheckFileType("image/"))
                        {
                            ModelState.AddModelError("Photo", "File type must be image");
                            return View();
                        }
                        if (!updatedHeaderBackground.BackgroundPhoto.CheckFileSize(600))
                        {
                            ModelState.AddModelError("LogoPhoto", "Image size must be max 600kb");
                            return View();
                        }
                        string oldPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbHeaderBackground.Value);
                        FileHelper.DeleteFile(oldPath);

                        dbHeaderBackground.Value = updatedHeaderBackground.BackgroundPhoto.CreateFile(_env, "assets/images");

                    }
                    else
                    {
                        HeaderBackground newHeaderBackground = new()
                        {
                            Value = dbHeaderBackground.Value
                        };
                    }
                }
                else
                {

                    if (dbHeaderBackground.Value.Trim().ToLower() == updatedHeaderBackground.Value.Trim().ToLower())
                    {

                        return RedirectToAction(nameof(Index));
                    }
                    dbHeaderBackground.Value = updatedHeaderBackground.Value;

                }
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
