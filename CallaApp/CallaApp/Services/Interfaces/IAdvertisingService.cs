using CallaApp.Models;

namespace CallaApp.Services.Interfaces
{
    public interface IAdvertisingService
    {
        Task<List<Advertising>> GetAllAsync();

        Task<Advertising> GetByIdAsync(int? id);
    }
}
