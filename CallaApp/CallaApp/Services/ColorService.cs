using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CallaApp.Services
{
    public class ColorService: IColorService
    {
        private readonly AppDbContext _context;
        public ColorService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Color>> GetAllAsync() => await _context.Colors.Include(m => m.ProductColors).ToListAsync();
        public async Task<Color> GetByIdAsync(int? id) => await _context.Colors.Include(m => m.ProductColors).FirstOrDefaultAsync(m => m.Id == id);
    }
}
