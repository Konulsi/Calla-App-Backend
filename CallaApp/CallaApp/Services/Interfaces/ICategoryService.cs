using CallaApp.Models;

namespace CallaApp.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int? id);
    }
}
