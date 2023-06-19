using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CallaApp.Services
{
    public class SliderService: ISliderService
    {
        private readonly AppDbContext _context;
        public SliderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Slider>> GetAll()
        {
            return await _context.Sliders.Where(m => !m.SoftDelete).ToListAsync();
        }

        public async Task<Slider> GetById(int? id)
        {
            return await _context.Sliders.Where(m => !m.SoftDelete).FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
