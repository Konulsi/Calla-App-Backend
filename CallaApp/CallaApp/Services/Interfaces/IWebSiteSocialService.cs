using CallaApp.Models;

namespace CallaApp.Services.Interfaces
{
    public interface IWebSiteSocialService
    {
        Task<List<WebSiteSocial>> GetAllAsync();
        Task<WebSiteSocial> GetByIdAsync(int? id);
    }
}
