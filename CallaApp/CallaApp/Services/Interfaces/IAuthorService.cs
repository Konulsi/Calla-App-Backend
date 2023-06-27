using CallaApp.Models;

namespace CallaApp.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAllAsync();
        Task<Author> GetByIdAsync(int? id);
    }
}
