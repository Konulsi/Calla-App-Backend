using CallaApp.Models;

namespace CallaApp.Services.Interfaces
{
    public interface ILayoutService
    {
        Dictionary<string, string> GetSettingsData();
        List<HeaderBackground> GetAllAsync();
        Task<HeaderBackground> GetByIdAsync(int? id);
        List<Settings> GetSettingDatas();
        Task<Settings> GetById(int? id);
    }
}
