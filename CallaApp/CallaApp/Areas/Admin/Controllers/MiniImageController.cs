using CallaApp.Areas.Admin.ViewModels.Banner;
using CallaApp.Areas.Admin.ViewModels.MiniImage;
using CallaApp.Areas.Admin.ViewModels.Slider;
using CallaApp.Data;
using CallaApp.Helpers;
using CallaApp.Models;
using CallaApp.Services;
using CallaApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CallaApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MiniImageController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMiniImageService _miniImageService;
        public MiniImageController(AppDbContext context,
                                    IWebHostEnvironment env,
                                    IMiniImageService miniImageService)
        {
            _context = context;
            _env = env;
            _miniImageService = miniImageService;
        }
        public async Task<IActionResult> Index()
        {
            List<MiniImage> miniImages = await _miniImageService.GetAllAsync();
            return View(miniImages);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MiniImageCreateVM miniImages)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                foreach (var photo in miniImages.Photos)
                {
                    if (!photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View();
                    }

                    if (!photo.CheckFileSize(600))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 600kb");
                        return View();
                    }
                }

                foreach (var photo in miniImages.Photos)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;

                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);

                    await FileHelper.SaveFileAsync(path, photo);

                    MiniImage newMiniImage = new()
                    {
                        Image = fileName
                    };

                    await _context.MiniImages.AddAsync(newMiniImage);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                MiniImage dbMiniImage = await _miniImageService.GetByIdAsync(id);
                if (dbMiniImage is null) return NotFound();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbMiniImage.Image);
                FileHelper.DeleteFile(path);

                _context.MiniImages.Remove(dbMiniImage);
                await _context.SaveChangesAsync();

                return Ok();
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
            MiniImage dbMiniImage = await _miniImageService.GetByIdAsync(id);
            if (dbMiniImage is null) return NotFound();
            MiniImageUpdateVM model = new()
            {
                Image = dbMiniImage.Image,
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, MiniImageUpdateVM miniImage)
        {
            try
            {
                if (id == null) return BadRequest();
                MiniImage dbMiniImage = await _miniImageService.GetByIdAsync(id);
                if (dbMiniImage is null) return NotFound();

                MiniImageUpdateVM model = new()
                {
                    Image = dbMiniImage.Image,
                };

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (miniImage.Photo != null)
                {

                    if (!miniImage.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(miniImage);
                    }
                    if (!miniImage.Photo.CheckFileSize(600))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 600kb");
                        return View(miniImage);
                    }

                    string oldPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbMiniImage.Image);
                    FileHelper.DeleteFile(oldPath);

                    string fileName = Guid.NewGuid().ToString() + "_" + miniImage.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);
                    await FileHelper.SaveFileAsync(newPath, miniImage.Photo);

                    dbMiniImage.Image = fileName;
                }
                else
                {
                    MiniImageUpdateVM newMiniImage = new()
                    {
                        Image = dbMiniImage.Image
                    };
                }

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
