using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CallaApp.Services
{
    public class BrandService: IBrandService
    {
        private readonly AppDbContext _context;
        public BrandService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Brand>> GetAllAsync() => await _context.Brands.Include(b => b.Products).ToListAsync();
        public async Task<Brand> GetByIdAsync(int? id) => await _context.Brands.FirstOrDefaultAsync(m => m.Id == id);
    }
}
