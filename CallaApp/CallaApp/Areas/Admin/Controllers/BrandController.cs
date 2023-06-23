using CallaApp.Areas.Admin.ViewModels.Advertising;
using CallaApp.Areas.Admin.ViewModels.Brand;
using CallaApp.Data;
using CallaApp.Helpers;
using CallaApp.Models;
using CallaApp.Services;
using CallaApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IBrandService _brandService;
        public BrandController(AppDbContext context,
                               IWebHostEnvironment env,
                               IBrandService brandService)
        {
            _context = context;
            _env = env;
            _brandService = brandService;
        }
        public async Task<IActionResult> Index()
        {
            List<Brand> brands = await _brandService.GetAllAsync();
            return View(brands);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandCreateVM brandCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(brandCreate);
                }

                if (!brandCreate.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(brandCreate);
                }

                if (!brandCreate.Photo.CheckFileSize(600))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 600kb");
                    return View(brandCreate);
                }

                string fileName = Guid.NewGuid().ToString() + "_" + brandCreate.Photo.FileName;
                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);
                await FileHelper.SaveFileAsync(path, brandCreate.Photo);

                Brand newBrand = new()
                {
                    Image = fileName,
                    Name = brandCreate.Name,
                };

                await _context.Brands.AddAsync(newBrand);
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
            if (id is null) return BadRequest();
            Brand dbBrand = await _brandService.GetByIdAsync(id);
            if (dbBrand is null) return NotFound();

            BrandUpdateVM model = new()
            {
                Image = dbBrand.Image,
                Name = dbBrand.Name
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, BrandUpdateVM updateBrand)
        {
            try
            {
                if (id is null) return BadRequest();
                Brand dbBrand = await _brandService.GetByIdAsync(id);
                if (dbBrand is null) return NotFound();

                BrandUpdateVM model = new()
                {
                    Image = dbBrand.Image,
                    Name = dbBrand.Name
                };

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (updateBrand.Photo != null)
                {

                    if (!updateBrand.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(updateBrand);
                    }
                    if (!updateBrand.Photo.CheckFileSize(600))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 600kb");
                        return View(updateBrand);
                    }

                    string oldPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbBrand.Image);
                    FileHelper.DeleteFile(oldPath);

                    string fileName = Guid.NewGuid().ToString() + "_" + updateBrand.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);

                    await FileHelper.SaveFileAsync(newPath, updateBrand.Photo);

                    dbBrand.Image = fileName;
                }
                else
                {
                    BrandUpdateVM newSlider = new()
                    {
                        Image = dbBrand.Image
                    };
                }
                dbBrand.Name = updateBrand.Name;

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
                Brand dbBrand = await _brandService.GetByIdAsync(id);
                if (dbBrand is null) return NotFound();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/", dbBrand.Image);
                FileHelper.DeleteFile(path);

                _context.Brands.Remove(dbBrand);
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
