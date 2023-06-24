using CallaApp.Areas.Admin.ViewModels.About;
using CallaApp.Areas.Admin.ViewModels.Advertising;
using CallaApp.Data;
using CallaApp.Helpers;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IAboutService _aboutService;
        public AboutController(AppDbContext context,
                               IWebHostEnvironment env,
                               IAboutService aboutService)
        {
            _context = context;
            _env = env;
            _aboutService = aboutService;
        }
        public async Task<IActionResult> Index()
        {
            List<About> abouts = await _aboutService.GetAllAsync();
            return View(abouts);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            About dbAbout = await _aboutService.GetByIdAsync(id);
            if (dbAbout is null) return NotFound();

            return View(dbAbout);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutCreateVM aboutCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(aboutCreate);
                }

                if (!aboutCreate.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(aboutCreate);
                }

                if (!aboutCreate.Photo.CheckFileSize(600))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 600kb");
                    return View(aboutCreate);
                }

                string fileName = Guid.NewGuid().ToString() + "_" + aboutCreate.Photo.FileName;
                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);
                await FileHelper.SaveFileAsync(path, aboutCreate.Photo);

                About newAbout = new()
                {
                    Image = fileName,
                    Title = aboutCreate.Title,
                    Description = aboutCreate.Description,
                };

                await _context.Abouts.AddAsync(newAbout);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }





        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();
            About dbAbout = await _aboutService.GetByIdAsync(id);
            if (dbAbout is null) return NotFound();

            AboutUpdateVM model = new()
            {
                Image = dbAbout.Image,
                Title = dbAbout.Title,
                Description = dbAbout.Description,
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, AboutUpdateVM aboutUpdate)
        {
            try
            {
                if (id == null) return BadRequest();
                About dbAbout = await _aboutService.GetByIdAsync(id);
                if (dbAbout is null) return NotFound();

                AboutUpdateVM model = new()
                {
                    Image = dbAbout.Image,
                    Title = dbAbout.Title,
                    Description = dbAbout.Description,
                };

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (aboutUpdate.Photo != null)
                {

                    if (!aboutUpdate.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(aboutUpdate);
                    }
                    if (!aboutUpdate.Photo.CheckFileSize(600))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 600kb");
                        return View(aboutUpdate);
                    }

                    string oldPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbAbout.Image);
                    FileHelper.DeleteFile(oldPath);

                    string fileName = Guid.NewGuid().ToString() + "_" + aboutUpdate.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);

                    await FileHelper.SaveFileAsync(newPath, aboutUpdate.Photo);

                    dbAbout.Image = fileName;
                }
                else
                {
                    AboutUpdateVM newAbout = new()
                    {
                        Image = dbAbout.Image
                    };
                }

                dbAbout.Title = aboutUpdate.Title;
                dbAbout.Description = aboutUpdate.Description;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }




        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                About dbAbout = await _aboutService.GetByIdAsync(id);
                if (dbAbout is null) return NotFound();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbAbout.Image);
                FileHelper.DeleteFile(path);

                _context.Abouts.Remove(dbAbout);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }
    }
}
