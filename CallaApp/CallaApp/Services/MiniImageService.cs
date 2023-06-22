using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CallaApp.Services
{
    public class MiniImageService: IMiniImageService
    {
        private readonly AppDbContext _context;
        public MiniImageService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<MiniImage>> GetAllAsync() => await _context.MiniImages.ToListAsync();
        public async Task<MiniImage> GetByIdAsync(int? id) => await _context.MiniImages.FirstOrDefaultAsync(m => m.Id == id);
    }
}
