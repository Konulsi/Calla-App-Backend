using CallaApp.Data;
using CallaApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CallaApp.Services
{
    public class PositionService
    {
        private readonly AppDbContext _context;
        public PositionService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Position>> GetAllAsync() => await _context.Positions.ToListAsync();
        public async Task<Position> GetByIdAsync(int? id) => await _context.Positions.FirstOrDefaultAsync(m => m.Id == id);
    }
}
