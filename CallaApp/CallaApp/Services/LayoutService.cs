using CallaApp.Data;
using CallaApp.Services.Interfaces;

namespace CallaApp.Services
{
    public class LayoutService: ILayoutService
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }

        public Dictionary<string, string> GetSettingsData()
        {
            Dictionary<string, string> settings = _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);

            return settings;
        }
    }
}
