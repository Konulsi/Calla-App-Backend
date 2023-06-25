using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CallaApp.Services
{
    public class PositionService: IPositionService
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
