using CallaApp.Models;

namespace CallaApp.Services.Interfaces
{
    public interface IMiniImageService
    {
        Task<List<MiniImage>> GetAllAsync();

        Task<MiniImage> GetByIdAsync(int? id);
    }
}
