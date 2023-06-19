using CallaApp.Models;

namespace CallaApp.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<Slider>> GetAllAsync();
        Task<Slider> GetByIdAsync(int? id);
    }
}
