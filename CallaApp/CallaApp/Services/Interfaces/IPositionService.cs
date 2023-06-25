using CallaApp.Models;

namespace CallaApp.Services.Interfaces
{
    public interface IPositionService
    {
        Task<List<Position>> GetAllAsync();
        Task<Position> GetByIdAsync(int? id);
    }
}
