using CallaApp.Models;

namespace CallaApp.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetByIdAsync(int id);
        Task<List<Product>> GetAllAsync();
        Task<Product> GetFullDataByIdAsync(int? id);
        Task<List<Product>> GetFeaturedProducts();
        Task<List<Product>> GetBestsellerProducts();
        Task<List<Product>> GetLatestProducts();
        Task<List<Product>> GetNewProducts();
        Task<int> GetCountAsync();
        Task<List<Product>> GetPaginatedDatas(int page, int take, int? cateId, int? tagId);
    }
}
