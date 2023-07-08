using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using CallaApp.ViewModels.Product;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace CallaApp.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetAllAsync() => await _context.Products
                                                                    .Include(m => m.Images)
                                                                    .Include(m => m.ProductSizes)
                                                                    .ThenInclude(m => m.Size)
                                                                    .Include(m => m.Brand)
                                                                    .Include(m => m.ProductTags)
                                                                    .ThenInclude(m => m.Tag)
                                                                    .Include(m => m.ProductColors)
                                                                    .ThenInclude(m => m.Color)
                                                                    .Include(m => m.ProductComments)
                                                                    .Include(m => m.ProductCategories)
                                                                    .ThenInclude(m => m.Category)
                                                                    .ToListAsync();
        public async Task<Product> GetFullDataByIdAsync(int? id) => await _context.Products
                                                                            .Include(m => m.Images)
                                                                            .Include(m => m.ProductSizes)
                                                                            .ThenInclude(m => m.Size)
                                                                            .Include(m => m.Brand)
                                                                            .Include(m => m.ProductTags)
                                                                            .ThenInclude(m => m.Tag)
                                                                            .Include(m => m.ProductColors)
                                                                            .ThenInclude(m => m.Color)
                                                                            .Include(m => m.ProductComments)
                                                                            .Include(m => m.ProductCategories)
                                                                            .ThenInclude(m => m.Category)
                                                                            .FirstOrDefaultAsync(m => m.Id == id);
        public async Task<Product> GetByIdAsync(int? id) => await _context.Products
                                                                        .Include(p => p.Images)
                                                                        .Include(m => m.ProductSizes)
                                                                        .Include(m => m.ProductCategories)
                                                                        .Include(m => m.ProductColors)
                                                                        .Include(m => m.ProductTags)
                                                                        .Include(m => m.Brand)
                                                                        .FirstOrDefaultAsync(p => p.Id == id);
        public async Task<List<ProductVM>> GetMappedAllProducts()
        {
            List<ProductVM> model = new();
            var products = await _context.Products.Include(p => p.Images).ToListAsync();
            foreach (var item in products)
            {
                model.Add(new ProductVM
                {
                    Id = item.Id,
                    Price = item.Price,
                    Name = item.Name,
                    ProductImages = item.Images,
                    //Rating = item.Rate,
                });
            }
            return model;
        }
        public async Task<int> GetCountAsync() => await _context.Products.CountAsync();
        public async Task<List<ProductVM>> GetDatasAsync()
        {
            List<ProductVM> model = new();
            var products = await _context.Products.Include(p => p.Images).ToListAsync();
            foreach (var item in products)
            {
                model.Add(new ProductVM
                {
                    Id = item.Id,
                    Price = item.Price,
                    Name = item.Name,
                    ProductImages = item.Images,
                    Rating = item.Rate
                });
            }
            return model;
        }

        public async Task<List<Product>> GetPaginatedDatasAsync(int page, int take,string sortValue,  int? cateId, int? tagId, int? colorId,int? sizeId, int? brandId, int? value1, int? value2)
        {
            List<Product> products = products = await _context.Products
                                                            .Include(p => p.Images)
                                                            .Include(p => p.ProductCategories)
                                                            .ThenInclude(pc => pc.Category)
                                                            .Include(p => p.ProductSizes)
                                                            .ThenInclude(ps => ps.Size)
                                                            .Include(p => p.ProductColors)
                                                            .ThenInclude(ps => ps.Color)
                                                            .Include(p => p.ProductTags)
                                                            .ThenInclude(ps => ps.Tag)
                                                            .Include(p => p.Brand)
                                                            .Skip((page * take) - take)
                                                            .Take(take)
                                                            .ToListAsync();

            if (sortValue != null)
            {

                if(sortValue == "Sort by Latest")
                {
                    return  await _context.Products
                                            .OrderBy(p => p.CreateDate).
                                             Skip((page * take) - take)
                                            .Take(take).ToListAsync();
                }

                if (sortValue == "Sort by Popularity")
                {
                    return await _context.Products
                                            .OrderByDescending(p => p.SaleCount).
                                             Skip((page * take) - take)
                                            .Take(take).ToListAsync();
                }

                if (sortValue == "Sort by Rated")
                {
                    return await _context.Products
                                            .OrderByDescending(p => p.Rate).
                                             Skip((page * take) - take)
                                            .Take(take).ToListAsync();
                }
                if (sortValue == "Sort by High Price")
                {
                    return await _context.Products
                                            .OrderByDescending(p => p.Price).
                                             Skip((page * take) - take)
                                            .Take(take).ToListAsync();
                }
                if (sortValue == "Sort by Low Price")
                {
                    return await _context.Products
                                            .OrderBy(p => p.Price).
                                             Skip((page * take) - take)
                                            .Take(take).ToListAsync();
                }
            }
            if (cateId != null)
            {
                return await _context.ProductCategory
                .Include(p => p.Product)
                .ThenInclude(p => p.Images)
                .Where(pc => pc.Category.Id == cateId)
                .Select(p => p.Product)
                .Skip((page * take) - take)
                .Take(take)
                .ToListAsync();
            }
            if (colorId != null)
            {
                return await _context.ProductColor
                        .Include(p => p.Product)
                        .ThenInclude(p => p.Images)
                        .Where(pc => pc.Color.Id == colorId)
                        .Select(p => p.Product)
                        .Skip((page * take) - take)
                        .Take(take)
                        .ToListAsync();
            }
            if (tagId != null)
            {
                return await _context.ProductTag
                .Include(p => p.Product)
                .ThenInclude(p => p.Images)
                .Where(pc => pc.Tag.Id == tagId)
                .Skip((page * take) - take)
                .Take(take)
                .Select(p => p.Product)
                .ToListAsync();
            }
            if (sizeId != null)
            {
                return await _context.ProductSize
                .Include(p => p.Product)
                .ThenInclude(p => p.Images)
                .Where(pc => pc.Size.Id == tagId)
                .Skip((page * take) - take)
                .Take(take)
                .Select(p => p.Product)
                .ToListAsync();
            }
            if (brandId != null)
            {
                return await _context.Products
                .Include(p => p.Images)
                .Include(c => c.Brand)
                .Where(p => p.Brand.Id == brandId)
                .Skip((page * take) - take)
                .Take(take)
                .ToListAsync();
            }
            if (value1 != null && value2 != null)
            {
                products = await _context.Products
               .Include(p => p.Images)
               .Where(p => p.Price >= value1 && p.Price <= value2)
               .Skip((page * take) - take)
               .Take(take)
               .ToListAsync();

            }


            return products;
        }


        public async Task<List<Product>> GetFeaturedProducts() => await _context.Products.OrderByDescending(m => m.Rate).ToListAsync();
        public async Task<List<Product>> GetBestsellerProducts() => await _context.Products.OrderByDescending(m => m.SaleCount).ToListAsync();
        public async Task<List<Product>> GetLatestProducts() => await _context.Products.OrderByDescending(m => m.CreateDate).ToListAsync();
        public async Task<List<Product>> GetNewProducts() => await _context.Products.Include(m => m.Images).OrderByDescending(m => m.CreateDate).Take(4).ToListAsync();
        public async Task<Product> GetProductByImageId(int? id)
        {
            return await _context.Products
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Images.Any(p => p.Id == id));
        }
        public async Task<ProductImage> GetImageById(int? id)
        {
            return await _context.ProductImages.FindAsync((int)id);
        }
        public async Task<List<ProductVM>> GetProductsByCategoryIdAsync(int? id, int page = 1, int take = 9)
        {
            List<ProductVM> model = new();
            var products = await _context.ProductCategory
                .Include(p => p.Product)
                .ThenInclude(p => p.Images)
                .Where(pc => pc.Category.Id == id)
                .Select(p => p.Product)
                .Skip((page * take) - take)
                .Take(take)
                .ToListAsync();

            foreach (var item in products)
            {
                model.Add(new ProductVM
                {
                    Id = item.Id,
                    Price = item.Price,
                    Name = item.Name,
                    ProductImages = item.Images,
                    Rating = item.Rate
                });
            }
            return model;
        }
        public async Task<List<ProductVM>> GetProductsBySizeIdAsync(int? id, int page = 1, int take = 9)
        {
            List<ProductVM> model = new();
            var products = await _context.ProductSize
                .Include(p => p.Product)
                .ThenInclude(p => p.Images)
                .Where(pc => pc.Size.Id == id)
                .Select(p => p.Product)
                .Skip((page * take) - take)
                .Take(take)
                .ToListAsync();

            foreach (var item in products)
            {
                model.Add(new ProductVM
                {
                    Id = item.Id,
                    Price = item.Price,
                    Name = item.Name,
                    ProductImages = item.Images,
                    Rating = item.Rate
                });
            }
            return model;
        }
        public async Task<List<ProductVM>> GetProductsByBrandIdAsync(int? id, int page = 1, int take = 9)
        {
            List<ProductVM> model = new();
            var products = await _context.Products
                .Include(p => p.Images)
                .Include(c => c.Brand)
                .Where(p => p.Brand.Id == id)
                .Skip((page * take) - take)
                .Take(take)
                .ToListAsync();

            foreach (var item in products)
            {
                model.Add(new ProductVM
                {
                    Id = item.Id,
                    Price = item.Price,
                    Name = item.Name,
                    ProductImages = item.Images,
                    Rating = item.Rate
                });
            }
            return model;
        }
        public async Task<List<ProductVM>> GetProductsByColorIdAsync(int? id, int page = 1, int take = 9)
        {
            List<ProductVM> model = new();
            var products = await _context.ProductColor
                .Include(p => p.Product)
                .ThenInclude(p => p.Images)
                .Where(pc => pc.Color.Id == id)
                .Select(p => p.Product)
                .Skip((page * take) - take)
                .Take(take)
                .ToListAsync();
            foreach (var item in products)
            {
                model.Add(new ProductVM
                {
                    Id = item.Id,
                    Price = item.Price,
                    Name = item.Name,
                    ProductImages = item.Images,
                    Rating = item.Rate
                });
            }
            return model;
        }

        public async Task<List<ProductVM>> GetProductsByTagIdAsync(int? id, int page = 1, int take = 9)
        {
            List<ProductVM> model = new();
            var products = await _context.ProductTag
                .Include(p => p.Product)
                .ThenInclude(p => p.Images)
                .Where(pc => pc.Tag.Id == id)
                .Select(p => p.Product)
                .Skip((page * take) - take)
                .Take(take)
                .ToListAsync();

            foreach (var item in products)
            {
                model.Add(new ProductVM
                {
                    Id = item.Id,
                    Price = item.Price,
                    Name = item.Name,
                    ProductImages = item.Images,
                    Rating = item.Rate
                });
            }
            return model;
        }

        public async Task<List<ProductVM>> GetProductsBySortAsync(string sortValue, int page = 1, int take = 9)
        {
            List<ProductVM> model = new();
            List<Product> products = new();
            if (sortValue == "Sort by Latest")
            {
                products= await _context.Products
                                        .OrderBy(p => p.CreateDate)
                                          .Skip((page * take) - take)
                .Take(take)
                .ToListAsync();

            }

            if (sortValue == "Sort by Popularity")
            {
                products = await _context.Products
                                         .Skip((page * take) - take)
                                            .Take(take)
                                            .ToListAsync();
            }

            if (sortValue == "Sort by Rated")
            {
                products = await _context.Products
                                        .OrderByDescending(p => p.Rate).Skip((page * take) - take)
                                            .Take(take)
                                            .ToListAsync();


            }
            if (sortValue == "Sort by High Price")
            {
                products = await _context.Products
                                        .OrderByDescending(p => p.Price)
                                                                                 .Skip((page * take) - take)
                                            .Take(take)
                                            .ToListAsync();
                ;
            }
            if (sortValue == "Sort by Low Price")
            {
                products = await _context.Products
                                        .OrderBy(p => p.Price)
                                            .Skip((page * take) - take)
                                            .Take(take)
                                            .ToListAsync();

            }



            foreach (var item in products)
            {
                model.Add(new ProductVM
                {
                    Id = item.Id,
                    Price = item.Price,
                    Name = item.Name,
                    ProductImages = item.Images,
                    Rating = item.Rate
                });
            }
            return model;
        }

        public async Task<int> GetProductsCountByRangeAsync(int? value1, int? value2)
        {
            return await _context.Products.Where(p => p.Price >= value1 && p.Price <= value2)
                                 .Include(p => p.Images)
                                 .CountAsync();
        }


        public void RemoveImage(ProductImage image)
        {
            _context.Remove(image);
        }
        public async Task<int> GetProductsCountByCategoryAsync(int? catId)
        {
            return await _context.ProductCategory
                 .Include(p => p.Product)
                 .ThenInclude(p => p.Images)
                 .Where(pc => pc.Category.Id == catId)
                 .Select(p => p.Product)
                 .CountAsync();
        }
        public async Task<int> GetProductsCountByColorAsync(int? colorId)
        {
            return await _context.ProductColor
                 .Include(p => p.Product)
                 .ThenInclude(p => p.Images)
                 .Where(pc => pc.Color.Id == colorId)
                 .Select(p => p.Product)
                 .CountAsync();
        }
        public async Task<int> GetProductsCountBySizeAsync(int? sizeId)
        {
            return await _context.ProductSize
                 .Include(p => p.Product)
                 .ThenInclude(p => p.Images)
                 .Where(pc => pc.Size.Id == sizeId)
                 .Select(p => p.Product)
                 .CountAsync();
        }
        public async Task<int> GetProductsCountByTagAsync(int? tagId)
        {
            return await _context.ProductTag
                  .Include(p => p.Product)
                  .ThenInclude(p => p.Images)
                  .Where(pc => pc.Tag.Id == (int)tagId)
                  .Select(p => p.Product)
                  .CountAsync();
        }
        public async Task<int> GetProductsCountByBrandAsync(int? brandId)
        {
            return await _context.Products
                   .Include(p => p.Images)
                   .Include(c => c.Brand)
                   .Where(p => p.Brand.Id == (int)brandId)
                   .CountAsync();
        }
        public async Task<int> GetProductsCountBySortText(string sortValue)
        {
           

                if(sortValue == "Sort by Latest")
                {
                    return  await _context.Products
                                            .OrderBy(p => p.CreateDate)
                                            .CountAsync();
                                            
                }

                if (sortValue == "Sort by Popularity")
                {
                    return await _context.Products
                                            .OrderByDescending(p => p.SaleCount)
                                             .CountAsync();
                }

                if (sortValue == "Sort by Rated")
                {
                    return await _context.Products
                                            .OrderByDescending(p => p.Rate)
                                            .CountAsync();
                }
                if (sortValue == "Sort by High Price")
                {
                    return await _context.Products
                                            .OrderByDescending(p => p.Price)
                                            .CountAsync();
                }
                if (sortValue == "Sort by Low Price")
                {
                    return await _context.Products
                                            .OrderBy(p => p.Price)
                                            .CountAsync();
                }


            return await  _context.Products.CountAsync();   
        }
        public async Task<List<Product>> GetRelatedProducts()
        {
            return await _context.Products
                 .Include(p => p.Images)
                 .OrderByDescending(p => p.Brand.Name)
                 .ToListAsync();
        }

        public async Task<List<ProductComment>> GetComments()
        {
            return await _context.ProductComments.Include(p => p.Product).ToListAsync();
        }
        public async Task<ProductComment> GetCommentByIdWithProduct(int? id)
        {
            return await _context.ProductComments.Include(p => p.Product).FirstOrDefaultAsync(pc => pc.Id == id);
        }
        public async Task<ProductComment> GetCommentById(int? id)
        {
            return await _context.ProductComments.FirstOrDefaultAsync(pc => pc.Id == id);
        }


    }
}
