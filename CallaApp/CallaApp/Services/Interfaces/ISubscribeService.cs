using CallaApp.Models;

namespace CallaApp.Services.Interfaces
{
    public interface ISubscribeService
    {
        Task<List<Subscribe>> GetAllAsync();
        Task<Subscribe> GetByIdAsync(int? id);
    }
}
