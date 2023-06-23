using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CallaApp.Services
{
    public class SizeService: ISizeService
    {
        private readonly AppDbContext _context;
        public SizeService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Size>> GetAllAsync() => await _context.Sizes.Include(m => m.ProductSizes).ToListAsync();
        public async Task<Size> GetByIdAsync(int? id) =>  await _context.Sizes.Include(m => m.ProductSizes).FirstOrDefaultAsync(m => m.Id == id);
    }
}
