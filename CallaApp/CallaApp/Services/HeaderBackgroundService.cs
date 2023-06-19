using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CallaApp.Services
{
    public class HeaderBackgroundService: IHeaderBackgroundService
    {
        private readonly AppDbContext _context;

        public HeaderBackgroundService(AppDbContext context)
        {
            _context = context;
        }

        public List<HeaderBackground> GetHeaderBackgroundsAsync() => _context.HeaderBackgrounds.Where(m => !m.SoftDelete).ToList();
        public async Task<HeaderBackground> GetHeaderBackgroundByIdAsync(int? id) => await _context.HeaderBackgrounds.Where(m => !m.SoftDelete).FirstOrDefaultAsync(m => m.Id == id);

    }
}
