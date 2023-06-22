using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CallaApp.Services
{
    public class DecorService: IDecorService
    {
        private readonly AppDbContext _context;
        public DecorService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Decor>> GetAllAsync() => await _context.Decors.ToListAsync();
        public async Task<Decor> GetByIdAsync(int? id) => await _context.Decors.FirstOrDefaultAsync(m => m.Id == id);
    }
}
