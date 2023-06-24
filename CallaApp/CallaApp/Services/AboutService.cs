using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CallaApp.Services
{
    public class AboutService: IAboutService
    {
        private readonly AppDbContext _context;
        public AboutService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<About>> GetAllAsync() => await _context.Abouts.ToListAsync();
        public async Task<About> GetByIdAsync(int? id) => await _context.Abouts.FirstOrDefaultAsync(m => m.Id == id);
    }
}
