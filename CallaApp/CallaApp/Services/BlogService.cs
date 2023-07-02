using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CallaApp.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;

        public BlogService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Blog>> GetAllAsync()
        {
            return await _context.Blogs.Include(b => b.BlogImage).Include(b => b.Author).Include(b => b.BlogComments).ToListAsync();
        }

        public async Task<Blog> GetBlogByImageId(int? id)
        {
            return await _context.Blogs.Include(b => b.BlogImage).FirstOrDefaultAsync(b => b.BlogImage.Any(bi => bi.Id == id));
        }

        public async Task<Blog> GetByIdAsync(int? id)
        {
            return await _context.Blogs.Include(b => b.BlogImage).Include(b => b.BlogComments).Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Blog>> GetLatestBlogs() => await _context.Blogs.OrderByDescending(m => m.CreateDate).ToListAsync();

        public async Task<BlogComment> GetCommentById(int? id)
        {
            return await _context.BlogComments.FindAsync(id);
        }

        public async Task<BlogComment> GetCommentByIdWithBlog(int? id)
        {
            return await _context.BlogComments.Include(b => b.Blog).FirstOrDefaultAsync(b => b.Id == id);
        }

        public  List<Blog> GetRelatedBlogs()
        {
            return  _context.Blogs
                 .Include(p => p.BlogImage)
                 .Include(p => p.Author)
                 .OrderByDescending(p => p.Author.Name).ToList();
        }


        public async Task<List<BlogComment>> GetComments()
        {
            return await _context.BlogComments.Include(b => b.Blog).ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Blogs.CountAsync();
        }

        public async Task<BlogImage> GetImageById(int? id)
        {
            return await _context.BlogImage.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Blog>> GetPaginatedDatasAsync(int page = 1, int take = 2)
        {
            return await _context.Blogs
                .Include(b => b.BlogImage)
                .Include(b => b.BlogComments)
                .Skip((page * take) - take)
                .Take(take)
                .ToListAsync();
        }

        public void RemoveImage(BlogImage img)
        {
            _context.BlogImage.Remove(img);
        }

    }
}
