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
        public async Task<List<Blog>> GetAllAsync() => await _context.Blogs
                                                                   .Include(m => m.BlogImage)
                                                                   .Include(m => m.Author)
                                                                   .Include(m => m.BlogComments)
                                                                   .ToListAsync();
        public async Task<Blog> GetByIdAsync(int? id) => await _context.Blogs
                                                                   .Include(m => m.BlogImage)
                                                                   .Include(m => m.Author)
                                                                   .Include(m => m.BlogComments)
                                                                   .FirstOrDefaultAsync(m => m.Id == id);
        public async Task<int> GetCountAsync() => await _context.Blogs.CountAsync();
        public async Task<List<Blog>> GetPaginatedDatasAsync(int page, int take)
        {
            return await _context.Blogs
                                .Include(m => m.BlogImage)
                                .Include(m => m.Author)
                                .Include(m => m.BlogComments)
                                .Skip((page * take) - take)
                                .Take(take).ToListAsync();

        }
    }
}
