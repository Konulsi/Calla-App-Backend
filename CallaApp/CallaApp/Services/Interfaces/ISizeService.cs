using CallaApp.Models;

namespace CallaApp.Services.Interfaces
{
    public interface ISizeService
    {
        Task<List<Size>> GetAllAsync();
        Task<Size> GetByIdAsync(int? id);
    }
}
