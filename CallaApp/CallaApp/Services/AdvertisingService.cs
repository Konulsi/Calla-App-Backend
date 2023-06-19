using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CallaApp.Services
{
    public class AdvertisingService: IAdvertisingService
    {
        private readonly AppDbContext _context;
        public AdvertisingService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Advertising>> GetAllAsync()
        {
            return await _context.Advertisings.Where(m => !m.SoftDelete).ToListAsync();

        }



        public async Task<Advertising> GetByIdAsync(int? id)
        {
            return await _context.Advertisings.Where(m => !m.SoftDelete).FirstOrDefaultAsync(m => m.Id == id);

        }
    }
}
