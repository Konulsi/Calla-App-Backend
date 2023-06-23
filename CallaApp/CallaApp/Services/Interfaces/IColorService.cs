using CallaApp.Models;

namespace CallaApp.Services.Interfaces
{
    public interface IColorService
    {
        Task<List<Color>> GetAllAsync();
        Task<Color> GetByIdAsync(int? id);
    }
}
