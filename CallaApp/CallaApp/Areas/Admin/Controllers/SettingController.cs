using CallaApp.Data;
using CallaApp.Helpers;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

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

                if (dbSetting.Value.Contains(".png") || dbSetting.Value.Contains(".jpg") || dbSetting.Value.Contains(".jpeg"))
                {
                    if (updatedSetting.LogoPhoto is not null)
                    {
                        if (!updatedSetting.LogoPhoto.CheckFileType("image/"))
                        {
                            ModelState.AddModelError("LogoPhoto", "File type must be image");
                            return View();
                        }
                        if (!updatedSetting.LogoPhoto.CheckFileSize(600))
                        {
                            ModelState.AddModelError("LogoPhoto", "Image size must be max 600kb");
                            return View();
                        }
                        string oldPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbSetting.Value);
                        FileHelper.DeleteFile(oldPath);
                        dbSetting.Value = updatedSetting.LogoPhoto.CreateFile(_env, "assets/images");
                    }
                    else
                    {
                        Settings newSetting = new()
                        {
                            Value = dbSetting.Value
                        };
                    }

                    if (updatedSetting.PaymentPhoto is not null)
                    {

                        if (!updatedSetting.PaymentPhoto.CheckFileType("image/"))
                        {
                            ModelState.AddModelError("PaymentPhoto", "File type must be image");
                            return View();
                        }
                        if (!updatedSetting.LogoPhoto.CheckFileSize(600))
                        {
                            ModelState.AddModelError("PaymentPhoto", "Image size must be max 600kb");
                            return View();
                        }
                        string oldPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbSetting.Value);
                        FileHelper.DeleteFile(oldPath);
                        dbSetting.Value = updatedSetting.PaymentPhoto.CreateFile(_env, "assets/images");
                    }
                    else
                    {
                        Settings newSetting = new()
                        {
                            Value = dbSetting.Value
                        };
                    }
                }
                else
                {
                    if (dbSetting.Value.Trim().ToLower() == updatedSetting.Value.Trim().ToLower())
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    dbSetting.Value = updatedSetting.Value;
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
