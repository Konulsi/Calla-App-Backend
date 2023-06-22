using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CallaApp.Services
{
    public class BannerService: IBannerService
    {
        private readonly AppDbContext _context;
        public BannerService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Banner>> GetAllAsync() => await _context.Banners.ToListAsync();
        public async Task<Banner> GetByIdAsync(int? id) => await _context.Banners.FirstOrDefaultAsync(m => m.Id == id);
    }
}
