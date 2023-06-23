using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CallaApp.Services
{
    public class TagService: ITagService
    {
        private readonly AppDbContext _context;
        public TagService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Tag>> GetAllAsync() => await _context.Tags.Include(m => m.ProductTags).ToListAsync();
        public async Task<Tag> GetByIdAsync(int? id) => await _context.Tags.Include(m => m.ProductTags).FirstOrDefaultAsync(m => m.Id == id);
    }
}
