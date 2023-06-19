using CallaApp.Models;

namespace CallaApp.Services.Interfaces
{
    public interface IBannerService
    {
        Task<List<Banner>> GetAllAsync();

        Task<Banner> GetByIdAsync(int? id);
    }
}
