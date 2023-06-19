using CallaApp.Models;

namespace CallaApp.Services.Interfaces
{
    public interface IDecorService
    {
        Task<List<Decor>> GetAllAsync();

        Task<Decor> GetByIdAsync(int? id);
    }
}
