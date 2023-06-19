using CallaApp.Models;

namespace CallaApp.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<Slider>> GetAll();
        Task<Slider> GetById(int? id);
    }
}
