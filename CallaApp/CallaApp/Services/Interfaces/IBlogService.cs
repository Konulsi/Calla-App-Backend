using CallaApp.Models;

namespace CallaApp.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<Blog>> GetAllAsync();
        Task<int> GetCountAsync();
        Task<List<Blog>> GetPaginatedDatasAsync(int page, int take);
        Task<Blog> GetByIdAsync(int? id);
    }
}
