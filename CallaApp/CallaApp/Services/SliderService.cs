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
        public async Task<List<Slider>> GetAllAsync() => await _context.Sliders.ToListAsync();
        public async Task<Slider> GetByIdAsync(int? id) => await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
    }
}
