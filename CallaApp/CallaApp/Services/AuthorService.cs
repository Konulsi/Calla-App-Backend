using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CallaApp.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext _context;
        public AuthorService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Author>> GetAllAsync() => await _context.Authors.Include(m => m.Blogs).ToListAsync();
        public async Task<Author> GetByIdAsync(int? id) => await _context.Authors.FirstOrDefaultAsync(m => m.Id == id);

    }
}
