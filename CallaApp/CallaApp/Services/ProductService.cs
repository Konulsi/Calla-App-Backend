﻿using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CallaApp.Services
{
    public class ProductService: IProductService
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
                                                                    //.Include(m => m.ProductComments)
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
                                                                            //.Include(m => m.ProductComments)
                                                                            .Include(m => m.ProductCategories)
                                                                            .ThenInclude(m => m.Category)
                                                                            .FirstOrDefaultAsync(m => m.Id == id);

        public async Task<Product> GetByIdAsync(int id) => await _context.Products.FindAsync(id);
        public async Task<int> GetCountAsync() => await _context.Products.CountAsync();


        public async Task<List<Product>> GetFeaturedProducts() => await _context.Products.Where(m => !m.SoftDelete).OrderByDescending(m => m.Rate).ToListAsync();
        public async Task<List<Product>> GetBestsellerProducts() => await _context.Products.Where(m => !m.SoftDelete).OrderByDescending(m => m.SaleCount).ToListAsync();
        public async Task<List<Product>> GetLatestProducts() => await _context.Products.Where(m => !m.SoftDelete).OrderByDescending(m => m.CreateDate).ToListAsync();
        public async Task<List<Product>> GetNewProducts() => await _context.Products.Include(m => m.Images).Where(m => !m.SoftDelete).OrderByDescending(m => m.CreateDate).Take(4).ToListAsync();



        public async Task<List<Product>> GetPaginatedDatas(int page, int take, int? cateId, int? tagId)
        {
            List<Product> products = null;
            if (cateId == null || tagId == null)
            {
                products = await _context.Products
                    .Include(m => m.ProductSizes)
                    .Include(m => m.ProductTags)
                    .Include(m => m.Brand)
                    .Include(m => m.ProductColors)
                    //.Include(m => m.ProductComments)
                    .Include(m => m.ProductCategories)?
                    .Include(m => m.Images)
                    .Skip((page * take) - take)
                    .Take(take).ToListAsync();
            }
            else
            {
                products = await _context.ProductCategory
                                        .Include(m => m.Product)
                                        .ThenInclude(m => m.Images)
                                        .Where(m => m.Category.Id == cateId)
                                        .Select(m => m.Product)
                                        .Skip((page * take) - take).Take(take).ToListAsync();
            }

            if (tagId != null)
            {
                products = await _context.ProductTag
                                        .Include(m => m.Product)
                                        .ThenInclude(m => m.Images)
                                        .Where(m => m.Tag.Id == tagId)
                                        .Select(m => m.Product)
                                        .Skip((page * take) - take).Take(take).ToListAsync();
            }

            return products;

        }

    }
}