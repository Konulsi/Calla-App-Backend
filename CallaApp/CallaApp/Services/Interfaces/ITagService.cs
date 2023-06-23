using CallaApp.Models;

namespace CallaApp.Services.Interfaces
{
    public interface ITagService
    {
        Task<List<Tag>> GetAllAsync();
        Task<Tag> GetByIdAsync(int? id);
    }
}
