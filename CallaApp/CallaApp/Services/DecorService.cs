using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CallaApp.Services
{
    public class DecorService: IDecorService
    {
        private readonly AppDbContext _context;
        public DecorService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Decor>> GetAllAsync()
        {
            return await _context.Decors.Where(m => !m.SoftDelete).ToListAsync();

        }



        public async Task<Decor> GetByIdAsync(int? id)
        {
            return await _context.Decors.Where(m => !m.SoftDelete).FirstOrDefaultAsync(m => m.Id == id);

        }
    }
}
