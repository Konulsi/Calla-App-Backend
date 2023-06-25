using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CallaApp.Services
{
    public class LayoutService: ILayoutService
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }
        public Dictionary<string, string> GetSettingsData() =>  _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);
        public Dictionary<string, string> GetHeaderBackgroundData() =>  _context.HeaderBackgrounds.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);
        public List<HeaderBackground> GetAllAsync() => _context.HeaderBackgrounds.ToList();
        public async Task<HeaderBackground> GetByIdAsync(int? id) => await _context.HeaderBackgrounds.FirstOrDefaultAsync(m => m.Id == id);
        public List<Settings> GetSettingDatas() => _context.Settings.ToList();
        public async Task<Settings> GetById(int? id) => await _context.Settings.FirstOrDefaultAsync(m => m.Id == id);
    }
}
