using CallaApp.Areas.Admin.ViewModels.Advertising;
using CallaApp.Areas.Admin.ViewModels.Author;
using CallaApp.Data;
using CallaApp.Helpers;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IAuthorService _authorService;
        public AuthorController(AppDbContext context,
                                IWebHostEnvironment env,
                                IAuthorService authorService)
        {
            _context = context;
            _env = env;
            _authorService = authorService;
        }
        public async Task<IActionResult> Index()
        {
            List<Author> authors = await _authorService.GetAllAsync();
            return View(authors);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            Author dbAuthor = await _authorService.GetByIdAsync((int)id);
            if (dbAuthor is null) return NotFound();
            return View(dbAuthor);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorCreateVM authorCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(authorCreate);
                }

                if (!authorCreate.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(authorCreate);
                }

                if (!authorCreate.Photo.CheckFileSize(600))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 600kb");
                    return View(authorCreate);
                }

                string fileName = Guid.NewGuid().ToString() + "_" + authorCreate.Photo.FileName;
                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);
                await FileHelper.SaveFileAsync(path, authorCreate.Photo);

                Author newAuthor = new()
                {
                    Image = fileName,
                    Name = authorCreate.Name,
                    Email = authorCreate.Email,
                };

                await _context.Authors.AddAsync(newAuthor);
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
                Author dbAuthor = await _authorService.GetByIdAsync(id);
                if (dbAuthor is null) return NotFound();
                _context.Authors.Remove(dbAuthor);
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
            Author dbAuthor = await _authorService.GetByIdAsync(id);
            if (dbAuthor is null) return NotFound();

            AuthorUpdateVM model = new()
            {
                Name = dbAuthor.Name,
                Image = dbAuthor.Image,
                Email = dbAuthor.Email,
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, AuthorUpdateVM authorUpdate)
        {
            try
            {
                if (id == null) return BadRequest();
                Author dbAuthor = await _authorService.GetByIdAsync(id);
                if (dbAuthor is null) return NotFound();

                AuthorUpdateVM model = new()
                {
                    Name = dbAuthor.Name,
                    Image = dbAuthor.Image,
                    Email = dbAuthor.Email,
                };


                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (authorUpdate.Photo != null)
                {

                    if (!authorUpdate.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(authorUpdate);
                    }
                    if (!authorUpdate.Photo.CheckFileSize(600))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 600kb");
                        return View(authorUpdate);
                    }

                    string oldPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbAuthor.Image);
                    FileHelper.DeleteFile(oldPath);

                    string fileName = Guid.NewGuid().ToString() + "_" + authorUpdate.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);

                    await FileHelper.SaveFileAsync(newPath, authorUpdate.Photo);

                    dbAuthor.Image = fileName;
                }
                else
                {
                    Author newAuthor = new()
                    {
                        Image = dbAuthor.Image
                    };
                }

                dbAuthor.Name = authorUpdate.Name;
                dbAuthor.Email = authorUpdate.Email;

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
