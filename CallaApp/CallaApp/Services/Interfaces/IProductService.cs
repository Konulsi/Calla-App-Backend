using CallaApp.Models;
using CallaApp.ViewModels.Product;

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
        void RemoveImage(ProductImage image);
        Task<Product> GetProductByImageId(int? id);
        Task<List<Product>> GetPaginatedDatasAsync(int page, int take, int? categoryId, int? colorId, int? tagId, int? brandId, int? sizeId);
        Task<List<ProductVM>> GetProductsByCategoryIdAsync(int? id, int page = 1, int take = 6);
        Task<List<ProductVM>> GetProductsByColorIdAsync(int? id, int page = 1, int take = 6);
        Task<List<ProductVM>> GetProductsByBrandIdAsync(int? id, int page = 1, int take = 6);
        Task<List<ProductVM>> GetProductsBySizeIdAsync(int? id, int page = 1, int take = 6);
        Task<List<ProductVM>> GetProductsByTagIdAsync(int? id, int page = 1, int take = 6);
        //Task<IEnumerable<ProductVM>> GetDatasAsync();
        Task<int> GetProductsCountByCategoryAsync(int? catId);
        Task<int> GetProductsCountByBrandAsync(int? brandId);
        Task<int> GetProductsCountByColorAsync(int? colorId);
        Task<int> GetProductsCountByTagAsync(int? tagId);
        Task<int> GetProductsCountBySizeAsync(int? sizeId);
        Task<IEnumerable<Product>> GetRelatedProducts();
        Task<List<Product>> GetAllBySearchText(string searchText);
        Task<List<ProductComment>> GetComments();
        Task<ProductComment> GetCommentByIdWithProduct(int? id);
        Task<ProductComment> GetCommentById(int? id);
    }
}
