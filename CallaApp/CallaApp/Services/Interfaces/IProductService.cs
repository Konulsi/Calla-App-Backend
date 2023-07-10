using CallaApp.Models;
using CallaApp.ViewModels.Product;

namespace CallaApp.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetByIdAsync(int? id);
        Task<List<Product>> GetAllAsync();
        Task<Product> GetFullDataByIdAsync(int? id);
        Task<List<ProductVM>> GetDatasAsync();
        Task<List<ProductVM>> GetMappedAllProducts();

        Task<List<Product>> GetNewProducts();
        Task<int> GetCountAsync();

        void RemoveImage(ProductImage image);
        Task<Product> GetProductByImageId(int? id);
        Task<ProductImage> GetImageById(int? id);

        Task<List<Product>> GetPaginatedDatasAsync(int page, int take, string sortValue, string searchText, int? categoryId, int? colorId, int? tagId, int? brandId, int? sizeId, int? value1, int? value2);
        
        Task<List<ProductVM>> GetProductsByCategoryIdAsync(int? id, int page = 1, int take = 9);
        Task<List<ProductVM>> GetProductsByColorIdAsync(int? id, int page = 1, int take = 9);
        Task<List<ProductVM>> GetProductsByBrandIdAsync(int? id, int page = 1, int take = 9);
        Task<List<ProductVM>> GetProductsBySizeIdAsync(int? id, int page = 1, int take = 9);
        Task<List<ProductVM>> GetProductsByTagIdAsync(int? id, int page = 1, int take = 9);


        //Task<List<ProductVM>> GetProductsBySortAsync(string value, int page = 1, int take = 9);
        Task<int> GetProductsCountByCategoryAsync(int? catId);
        Task<int> GetProductsCountByBrandAsync(int? brandId);
        Task<int> GetProductsCountByColorAsync(int? colorId);
        Task<int> GetProductsCountByTagAsync(int? tagId);
        Task<int> GetProductsCountBySizeAsync(int? sizeId);

        Task<int> GetProductsCountByRangeAsync(int? value1, int? value2);
        Task<int> GetProductsCountBySearchTextAsync(string searchText);
        Task<int> GetProductsCountBySortTextAsync(string sortValue);

        Task<List<Product>> GetRelatedProducts();
        Task<List<ProductComment>> GetComments();
        Task<ProductComment> GetCommentByIdWithProduct(int? id);
        Task<ProductComment> GetCommentById(int? id);
    }
}
