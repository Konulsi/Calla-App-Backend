using CallaApp.Data;
using CallaApp.Helpers;
using CallaApp.Models;
using CallaApp.Services;
using CallaApp.Services.Interfaces;
using CallaApp.ViewModels.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CallaApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILayoutService _layoutService;
        private readonly ITagService _tagService;
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly IAuthorService _authorService;
        private readonly IProductService _productService;
        private readonly IMiniImageService _miniImageService;
        private readonly IWebSiteSocialService _webSiteSocialService;
        public BlogController(AppDbContext context,
                              ICategoryService categoryService,
                              ITagService tagService,
                              IMiniImageService miniImageService,
                              IBlogService blogService,
                              IAuthorService authorService,
                              IProductService productService,
                              ILayoutService layoutService,
                              IWebSiteSocialService webSiteSocialService)
        {
            _categoryService = categoryService;
            _tagService = tagService;
            _miniImageService = miniImageService;
            _context = context;
            _blogService = blogService;
            _authorService = authorService;
            _productService = productService;
            _layoutService = layoutService;
            _webSiteSocialService = webSiteSocialService;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 2)
        {
            List<Blog> blogs = await _blogService.GetAllAsync();
            List<Blog> latesBlog = await _blogService.GetLatestBlogs();
            List<Tag> tags = await _tagService.GetAllAsync();
            List<Category> categories = await _categoryService.GetAllAsync();
            List<Author> authors = await _authorService.GetAllAsync();
            List<MiniImage> miniImages = await _miniImageService.GetAllAsync();
            Dictionary<string, string> headerBackgrounds = _context.HeaderBackgrounds.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);

            List<Blog> datas = await _blogService.GetPaginatedDatasAsync(page, take);
            int pageCount = await GetPageCountAsync(take);
            Paginate<Blog> paginatedDatas = new(datas, page, pageCount);

            BlogVM model = new()
            {
                Tags = tags,
                Categories = categories,
                HeaderBackgrounds = headerBackgrounds,
                MiniImages = miniImages,
                Blogs = blogs,
                Authors = authors,
                PaginateDatas = paginatedDatas,
                LatesBlog = latesBlog,
                CountProducts = await _productService.GetCountAsync()
            };

            return View(model);
        }


        private async Task<int> GetPageCountAsync(int take)
        {
            int prodCount = await _blogService.GetCountAsync();
            return (int)Math.Ceiling((decimal)prodCount / take);
        }


        [HttpGet]
        public async Task<IActionResult> BlogDetail(int? id)
        {
            if (id is null) return BadRequest();
            var dbBlog = await _blogService.GetByIdAsync((int)id);
            if (dbBlog is null) return NotFound();
            var blogs = await _blogService.GetAllAsync();

            BlogDetailVM model = new()
            {
                Blogs = blogs.ToList(),
                Categories = await _categoryService.GetAllAsync(),
                Tags = await _tagService.GetAllAsync(),
                Products = await _productService.GetAllAsync(),
                BlogComments = blogs.FirstOrDefault().BlogComments,
                SingleBlog = await _blogService.GetByIdAsync(id),
                HeaderBackgrounds = _layoutService.GetHeaderBackgroundData(),
                Socials = await _webSiteSocialService.GetAllAsync(),
                Authors = await _authorService.GetAllAsync(),
                MiniImages = await _miniImageService.GetAllAsync(),
                LatesBlog = await _blogService.GetLatestBlogs(),
                RelatedBlogs =  _blogService.GetRelatedBlogs(),
                CountProducts = await _productService.GetCountAsync()
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> PostComment(BlogVM model, int? id, string userId)
        {
            if (id is null || userId == null) return BadRequest();
            if (!ModelState.IsValid) return RedirectToAction(nameof(BlogDetail), new { id });

            BlogComment blogComment = new()
            {
                Name = model.BlogCommentVM.Name,
                Email = model.BlogCommentVM.Email,
                Message = model.BlogCommentVM.Message,
                AppUserId = userId,
                BlogId = (int)id
            };
            await _context.BlogComments.AddAsync(blogComment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(BlogDetail), new { id });
        }
    }
}
