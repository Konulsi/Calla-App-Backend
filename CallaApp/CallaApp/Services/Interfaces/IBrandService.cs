using CallaApp.Models;

namespace CallaApp.Services.Interfaces
{
    public interface IBrandService
    {
        Task<List<Brand>> GetAllAsync();
        Task<Brand> GetByIdAsync(int? id);
    }
}
