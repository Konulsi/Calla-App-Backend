using CallaApp.Areas.Admin.ViewModels.Blog;
using CallaApp.Data;
using CallaApp.Helpers;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CallaApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IBlogService _blogService;
        private readonly IAuthorService _authorService;

        public BlogController(AppDbContext context, 
                              IWebHostEnvironment env,
                              IBlogService blogService, 
                              IAuthorService authorService)
        {
            _context = context;
            _env = env;
            _blogService = blogService;
            _authorService = authorService;
        }
        public async Task<IActionResult> Index()
        {
            List<Blog> blogs = await _blogService.GetAllAsync();
            return View(blogs);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Blog dbBlog = await _blogService.GetByIdAsync(id);
                if (dbBlog == null) return NotFound();

                BlogDetailVM model = new()
                {
                    Id = dbBlog.Id,
                    BlogImages = dbBlog.BlogImage.ToList(),
                    AuthorName = dbBlog.Author.Name,
                    Title = dbBlog.Title,
                    Description = dbBlog.Description
                };
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.authors = await GetAuthorsAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateVM blogCreate)
        {
            try
            {
                ViewBag.authors = await GetAuthorsAsync();

                if (!ModelState.IsValid)
                {
                    return View(blogCreate);
                }

                foreach (var photo in blogCreate.Photos)
                {
                    if (!photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photos", "File type must be image");
                        return View();
                    }
                    if (!photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photos", "Image size must be max 200kb");
                        return View();
                    }
                }
                List<BlogImage> blogImages = new();

                foreach (var photo in blogCreate.Photos)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;

                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);

                    await FileHelper.SaveFileAsync(path, photo);

                    BlogImage blogImage = new()
                    {
                        Image = fileName
                    };

                    blogImages.Add(blogImage);
                }

                Blog newBlog = new()
                {
                    Title = blogCreate.Title,
                    Description = blogCreate.Description,
                    AuthorId = blogCreate.AuthorId,
                    BlogImage = blogImages
                };

                await _context.BlogImage.AddRangeAsync(blogImages);
                await _context.Blogs.AddAsync(newBlog);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                throw;
            }

        }
        private async Task<SelectList> GetAuthorsAsync()
        {
            List<Author> authors = await _authorService.GetAllAsync();
            return new SelectList(authors, "Id", "Name");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Blog dbBlog = await _blogService.GetByIdAsync(id);
                if (dbBlog is null) return NotFound();

                ViewBag.authors = await GetAuthorsAsync();

                foreach (var item in dbBlog.BlogImage)
                {
                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", item.Image);

                    FileHelper.DeleteFile(path);
                }

                _context.Blogs.Remove(dbBlog);
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

            ViewBag.authors = await GetAuthorsAsync();

            Blog dbBlog = await _blogService.GetByIdAsync((int)id);

            if (dbBlog == null) return NotFound();


            BlogUpdateVM model = new()
            {
                Title = dbBlog.Title,
                AuthorId = dbBlog.AuthorId,
                BlogImages = dbBlog.BlogImage.ToList(),
                Description = dbBlog.Description
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, BlogUpdateVM updatedBlog)
        {

            if (id == null) return BadRequest();

            ViewBag.authors = await GetAuthorsAsync();

            Blog dbBlog = await _blogService.GetByIdAsync(id);

            if (dbBlog == null) return NotFound();

            BlogUpdateVM model = new()
            {
                Title = dbBlog.Title,
                AuthorId = dbBlog.AuthorId,
                BlogImages = dbBlog.BlogImage.ToList(),
                Description = dbBlog.Description
            };


            if (!ModelState.IsValid)
            {
                model.BlogImages = dbBlog.BlogImage.ToList();
                return View(model);
            }


            if (updatedBlog.Photos is not null)
            {
                foreach (var photo in updatedBlog.Photos)
                {
                    if (!photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }

                    if (!photo.CheckFileSize(600))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 600kb");
                        return View(model);
                    }
                }

                foreach (var item in dbBlog.BlogImage)
                {
                    string dbPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", item.Image);

                    FileHelper.DeleteFile(dbPath);
                }

                List<BlogImage> blogImages = new();

                foreach (var photo in updatedBlog.Photos)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);

                    await FileHelper.SaveFileAsync(path, photo);

                    BlogImage blogImage = new()
                    {
                        Image = fileName
                    };

                    blogImages.Add(blogImage);
                }

                await _context.BlogImage.AddRangeAsync(blogImages);
                dbBlog.BlogImage = blogImages;
            }
            else
            {
                Blog blog = new()
                {
                    BlogImage = dbBlog.BlogImage
                };
            }

            dbBlog.Title = updatedBlog.Title;
            dbBlog.Description = updatedBlog.Description;
            dbBlog.AuthorId = updatedBlog.AuthorId;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
