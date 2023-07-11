using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CallaApp.Services
{
    public class SubscribeService: ISubscribeService
    {
        private readonly AppDbContext _context;

        public SubscribeService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Subscribe>> GetAllAsync()
        {
            return await _context.Subscribes.ToListAsync();
        }
        public async Task<Subscribe> GetByIdAsync(int? id)
        {
            return await _context.Subscribes.FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
