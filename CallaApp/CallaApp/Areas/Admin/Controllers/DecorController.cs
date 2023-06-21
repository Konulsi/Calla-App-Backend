using CallaApp.Areas.Admin.ViewModels.Advertising;
using CallaApp.Areas.Admin.ViewModels.Decor;
using CallaApp.Data;
using CallaApp.Helpers;
using CallaApp.Models;
using CallaApp.Services;
using CallaApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509;

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
        public async Task<IActionResult> Create(DecorCreateVM model)
        {
            try
            {
                Decor decors = new();

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                foreach (var photo in model.Photos)
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

                foreach (var photo in model.Photos)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;

                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);

                    await FileHelper.SaveFileAsync(path, photo);

                    Decor newDecor = new()
                    {
                        Image = fileName,
                        HoverImage = fileName
                    };

                    await _context.Decors.AddAsync(newDecor);
                }

                await _context.SaveChangesAsync();


                decors.Image.FirstOrDefault().IsMain = true;
                newProduct.Images.Skip(1).FirstOrDefault().IsHover = true;

                await _context.Advertisings.AddAsync(newAdvertising);
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
