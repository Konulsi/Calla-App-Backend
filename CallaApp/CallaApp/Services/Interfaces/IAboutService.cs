using CallaApp.Models;

namespace CallaApp.Services.Interfaces
{
    public interface IAboutService
    {
        Task<List<About>> GetAllAsync();

        Task<About> GetByIdAsync(int? id);
    }
}
