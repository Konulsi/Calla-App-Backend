using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CallaApp.Services
{
    public class WebSiteSocialService: IWebSiteSocialService
    {
        private readonly AppDbContext _context;
        public WebSiteSocialService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<WebSiteSocial>> GetAllAsync() => await _context.WebSiteSocials.ToListAsync();
        public async Task<WebSiteSocial> GetByIdAsync(int? id) => await _context.WebSiteSocials.FirstOrDefaultAsync(m => m.Id == id);

    }
}
