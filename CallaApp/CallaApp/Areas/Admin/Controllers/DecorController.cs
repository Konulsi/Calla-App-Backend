using CallaApp.Areas.Admin.ViewModels.Advertising;
using CallaApp.Areas.Admin.ViewModels.Banner;
using CallaApp.Areas.Admin.ViewModels.Decor;
using CallaApp.Data;
using CallaApp.Helpers;
using CallaApp.Models;
using CallaApp.Services;
using CallaApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509;
using System.Reflection;

namespace CallaApp.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class DecorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IDecorService _decorService;
        public DecorController(AppDbContext context,
                                    IWebHostEnvironment env,
                                    IDecorService decorService)
        {
            _context = context;
            _env = env;
            _decorService = decorService;
        }
        public async Task<IActionResult> Index()
        {
            List<Decor> decor = await _decorService.GetAllAsync();
            return View(decor);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DecorCreateVM decor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(decor);
                }


                if (!decor.MainPhoto.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(decor);
                }

                if (!decor.MainPhoto.CheckFileSize(600))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 600kb");
                    return View(decor);
                }

                string fileName = Guid.NewGuid().ToString() + "_" + decor.MainPhoto.FileName;
                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);
                await FileHelper.SaveFileAsync(path, decor.MainPhoto);


                if (!ModelState.IsValid)
                {
                    return View(decor);
                }


                if (!decor.HoverPhoto.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(decor);
                }

                if (!decor.MainPhoto.CheckFileSize(600))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 600kb");
                    return View(decor);
                }

                string hoverFileName = Guid.NewGuid().ToString() + "_" + decor.HoverPhoto.FileName;
                string hoverPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", hoverFileName);
                await FileHelper.SaveFileAsync(hoverPath, decor.HoverPhoto);

                Decor newDecor = new()
                {
                    Image = fileName,
                    HoverImage = hoverFileName,
                };

                await _context.Decors.AddAsync(newDecor);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            Decor dbDecor = await _decorService.GetByIdAsync(id);
            if (dbDecor is null) return NotFound();

            DecorUpdateVM modelVM = new()
            {
                Image = dbDecor.Image,
                HoverImage = dbDecor.HoverImage,
            };
            return View(modelVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, DecorUpdateVM decor)
        {
            try
            {
                if (id is null) return BadRequest();
                Decor dbDecor = await _decorService.GetByIdAsync(id);
                if (dbDecor is null) return NotFound();

                DecorUpdateVM modelVM = new()
                {
                    Image = dbDecor.Image,
                    HoverImage = dbDecor.HoverImage,
                };

                if (!ModelState.IsValid)
                {
                    return View(modelVM);
                }

                if (decor.MainPhoto != null)
                {
                    if (!decor.MainPhoto.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("MainPhoto", "File type must be image");
                        return View(decor);
                    }
                    if (!decor.MainPhoto.CheckFileSize(600))
                    {
                        ModelState.AddModelError("MainPhoto", "Image size must be max 600kb");
                        return View(decor);
                    }

                    string oldMainImagePath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbDecor.Image);
                    FileHelper.DeleteFile(oldMainImagePath);

                    string MainImageNewfileName = Guid.NewGuid().ToString() + "_" + decor.MainPhoto.FileName;
                    string newMainImagePath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", MainImageNewfileName);
                    await FileHelper.SaveFileAsync(newMainImagePath, decor.MainPhoto);

                    dbDecor.Image = MainImageNewfileName;
                }
                else
                {
                    DecorUpdateVM newDecor = new()
                    {
                        Image = dbDecor.Image,
                    };
                }


                if (decor.HoverPhoto != null)
                {
                    if (!decor.HoverPhoto.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("HoverImage", "File type must be image");
                        return View(decor);
                    }
                    if (!decor.HoverPhoto.CheckFileSize(600))
                    {
                        ModelState.AddModelError("HoverImage", "Image size must be max 600kb");
                        return View(decor);
                    }
                    string oldHoverImagePath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbDecor.HoverImage);
                    FileHelper.DeleteFile(oldHoverImagePath);

                    string HoverImageNewfileName = Guid.NewGuid().ToString() + "_" + decor.HoverPhoto.FileName;
                    string newHoverImagePath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", HoverImageNewfileName);
                    await FileHelper.SaveFileAsync(newHoverImagePath, decor.HoverPhoto);

                    dbDecor.HoverImage = HoverImageNewfileName;
                }
                else
                {
                    DecorUpdateVM newDecor = new()
                    {
                        HoverImage = dbDecor.HoverImage,
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




        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id is null) return BadRequest();
                Decor dbDecor = await _decorService.GetByIdAsync(id);
                if (dbDecor is null) return NotFound();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbDecor.Image);
                FileHelper.DeleteFile(path);

                _context.Decors.Remove(dbDecor);
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
