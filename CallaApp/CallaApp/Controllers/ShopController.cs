using CallaApp.Data;
using CallaApp.Helpers;
using CallaApp.Models;
using CallaApp.Services;
using CallaApp.Services.Interfaces;
using CallaApp.ViewModels.Cart;
using CallaApp.ViewModels.Product;
using CallaApp.ViewModels.Shop;
using CallaApp.ViewModels.Wishlist;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Drawing;

namespace CallaApp.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ITagService _tagService;
        private readonly ISizeService _sizeService;
        private readonly IColorService _colorService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly ILayoutService _layoutService;
        private readonly ICartService _cartService;
        private readonly IWishlistService _wishlistService;
        public ShopController(ITagService tagService,
                              ISizeService sizeService,
                              ICategoryService categoryService,
                              IColorService colorService,
                              IBrandService brandService,
                              AppDbContext context,
                              IProductService productService,
                              ILayoutService layoutService,
                              ICartService cartService,
                              IWishlistService wishlistService)
        {
            _tagService = tagService;
            _sizeService = sizeService;
            _categoryService = categoryService;
            _colorService = colorService;
            _brandService = brandService;
            _context = context;
            _productService = productService;
            _layoutService = layoutService;
            _cartService = cartService;
            _wishlistService = wishlistService;
        }

        public async Task<IActionResult> Index(int page = 1, int take = 9,string sortValue = null, string searchText = null, int? categoryId = null, int? colorId = null, int? tagId = null, int? sizeId = null, int? brandId = null, int? value1 = null, int? value2 = null)
        {
            List<Product> datas = await _productService.GetPaginatedDatasAsync(page, take, sortValue, searchText, categoryId, colorId, tagId, sizeId, brandId, value1, value2);
            List<ProductVM> mappedDatas = GetMappedDatas(datas);
            ViewBag.catId = categoryId;
            ViewBag.tagId = tagId;
            ViewBag.sizeId = sizeId;
            ViewBag.colorId = colorId;
            ViewBag.brandId = brandId;
            ViewBag.value1 = value1;
            ViewBag.value2 = value2;
            ViewBag.searchText = searchText;
            ViewBag.sortValue = sortValue;

            int pageCount = 0;


            if (sortValue != null)
            {
                pageCount = await GetPageCountAsync(take, sortValue, null, null, null, null, null, null, null, null);
            }
            if (searchText != null)
            {
                pageCount = await GetPageCountAsync(take, null, searchText, null, null, null, null, null, null, null);
            }
            if (categoryId != null)
            {
                pageCount = await GetPageCountAsync(take,null, null, categoryId, null, null, null, null, null, null);
            }
            if (colorId != null)
            {
                pageCount = await GetPageCountAsync(take, null, null, null, colorId, null, null, null, null, null);
            }
            if (tagId != null)
            {
                pageCount = await GetPageCountAsync(take, null, null, null, null, tagId, null, null, null, null);
            }
            if (sizeId != null)
            {
                pageCount = await GetPageCountAsync(take, null, null, null, null, null, sizeId, null, null, null);
            }
            if (brandId != null)
            {
                pageCount = await GetPageCountAsync(take, null, null, null, null, null, null, brandId, null, null);
            }
            if (value1 != null && value2 != null)
            {
                pageCount = await GetPageCountAsync(take, null, null, null, null, null, null, null, value1, value2);
            }

            if (sortValue == null && searchText == null && categoryId == null && colorId == null && tagId == null && sizeId == null && brandId == null && value1 == null && value2 == null)
            {
                pageCount = await GetPageCountAsync(take, null, null, null, null, null, null, null, null, null);
            }

            Paginate<ProductVM> paginatedDatas = new(mappedDatas, page, pageCount);

            List<Tag> tags = await _tagService.GetAllAsync();
            List<Models.Size> sizes = await _sizeService.GetAllAsync();
            List<Models.Color> colors = await _colorService.GetAllAsync();
            List<Brand> brands = await _brandService.GetAllAsync();
            List<Category> categories = await _categoryService.GetAllAsync();

            ShopVM model = new()
            {
                Products = await _productService.GetAllAsync(),
                Tags = tags,
                Sizes = sizes,
                Categories = categories,
                Colors = colors,
                Brands = brands,
                HeaderBackgrounds = _layoutService.GetHeaderBackgroundData(),
                PaginateDatas = paginatedDatas,
                CountProducts = await _productService.GetCountAsync(),
            };
            return View(model);
        }


        private List<ProductVM> GetMappedDatas(List<Product> products)
        {
            List<ProductVM> mappedDatas = new();
            foreach (var product in products)
            {
                ProductVM productList = new()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    ProductImages = product.Images,
                };
                mappedDatas.Add(productList);
            }
            return mappedDatas;
        }

        private async Task<int> GetPageCountAsync(int take, string sortValue, string searchText, int? catId, int? colorId, int? tagId, int? sizeId, int? brandId, int? value1, int? value2)
        {
            int prodCount = 0;
            if (sortValue is not null)
            {
                prodCount = await _productService.GetProductsCountBySortTextAsync(sortValue);
            }
            if (searchText is not null)
            {
                prodCount = await _productService.GetProductsCountBySearchTextAsync(searchText);
            }
            if (catId is not null)
            {
                prodCount = await _productService.GetProductsCountByCategoryAsync(catId);
            }
            if (colorId is not null)
            {
                prodCount = await _productService.GetProductsCountByColorAsync(colorId);
            }
            if (tagId is not null)
            {
                prodCount = await _productService.GetProductsCountByTagAsync(tagId);
            }
            if (sizeId is not null)
            {
                prodCount = await _productService.GetProductsCountBySizeAsync(sizeId);
            }
            if (brandId is not null)
            {
                prodCount = await _productService.GetProductsCountByBrandAsync(brandId);
            }
            if (value1 != null && value2 != null)
            {
                prodCount = await _productService.GetProductsCountByRangeAsync(value1, value2); 
            }
            if (sortValue == null && searchText == null && catId == null && tagId == null && colorId == null && sizeId == null && brandId == null && value1 == null && value2 == null)
            {
                prodCount = await _productService.GetCountAsync();
            }

            return (int)Math.Ceiling((decimal)prodCount / take);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts(int page = 1, int take = 9)
        {
            int pageCount = await GetPageCountAsync(take,null, null, null, null, null, null, null, null, null);
            var products = await _productService.GetPaginatedDatasAsync(page, take,null, null, null, null, null, null, null, null, null);
            List<ProductVM> mappedDatas = GetMappedDatas(products);
            Paginate<ProductVM> paginatedDatas = new(mappedDatas, page, pageCount);

            return PartialView("_ProductListPartial", paginatedDatas);
        }

        [HttpGet]
        public async Task<IActionResult> GetRangeProducts(int value1, int value2, int page = 1, int take = 9)
        {
            List<Product> products = await _context.Products.Where(x => x.Price >= value1 && x.Price <= value2).Include(p => p.Images).ToListAsync();
            ViewBag.value1 = value1;
            ViewBag.value2 = value2;
            var productCount = products.Count();
            int pageCount = (int)Math.Ceiling((decimal)productCount / take);
            List<ProductVM> mappedDatas = GetMappedDatas(products);
            Paginate<ProductVM> paginatedDatas = new(mappedDatas, page, pageCount);
            return PartialView("_ProductListPartial", paginatedDatas);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByCategory(int? id, int page = 1, int take = 9)
        {
            if (id is null) return BadRequest();
            ViewBag.catId = id;

            var products = await _productService.GetProductsByCategoryIdAsync(id, page, take);

            int pageCount = await GetPageCountAsync(take,null, null, (int)id, null, null, null, null, null, null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductListPartial", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByColor(int? id, int page = 1, int take = 9)
        {
            if (id is null) return BadRequest();
            ViewBag.colorId = id;
            var products = await _productService.GetProductsByColorIdAsync(id);
            int pageCount = await GetPageCountAsync(take, null, null, null, (int)id, null, null, null, null, null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductListPartial", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByTag(int? id, int page = 1, int take = 9)
        {
            if (id is null) return BadRequest();
            ViewBag.tagId = id;

            var products = await _productService.GetProductsByTagIdAsync(id);

            int pageCount = await GetPageCountAsync(take, null, null, null, null, (int)id, null, null, null, null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductListPartial", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsBySize(int? id, int page = 1, int take = 9)
        {
            if (id is null) return BadRequest();
            ViewBag.sizeId = id;

            var products = await _productService.GetProductsBySizeIdAsync(id);

            int pageCount = await GetPageCountAsync(take, null, null, null, null, null, (int)id, null, null, null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductListPartial", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByBrand(int? id, int page = 1, int take = 9)
        {
            if (id is null) return BadRequest();
            ViewBag.brandId = id;

            var products = await _productService.GetProductsByBrandIdAsync(id);

            int pageCount = await GetPageCountAsync(take, null, null, null, null, null, null, (int)id, null, null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductListPartial", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetDatasAsync();
            return PartialView("_ProductListPartial", products);
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetail(int? id)
        {
            try
            {
                if (id is null) return BadRequest();
                var dbProduct = await _productService.GetFullDataByIdAsync((int)id);
                if (dbProduct is null) return NotFound();

                ProductDetailVM model = new()
                {
                    Id = dbProduct.Id,
                    ProductName = dbProduct.Name,
                    Price = dbProduct.Price,
                    ProductCategories = dbProduct.ProductCategories,
                    ProductTags = dbProduct.ProductTags,
                    ProductImages = dbProduct.Images,
                    ProductSizes = dbProduct.ProductSizes,
                    ProductColors = dbProduct.ProductColors,
                    SKU = dbProduct.SKU,
                    Rating = dbProduct.Rate,
                    Description = dbProduct.Description,
                    BrandName = dbProduct.Brand.Name,
                    HeaderBackgrounds = _layoutService.GetHeaderBackgroundData(),
                    RelatedProducts = await _productService.GetRelatedProducts(),
                    ProductCommentVM = new(),
                    ProductComments = dbProduct.ProductComments.ToList()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> PostComment(ProductDetailVM model, int? id, string userId)
        {
            if (id is null || userId == null) return BadRequest();
            if (!ModelState.IsValid) return RedirectToAction(nameof(ProductDetailVM), new { id });

            ProductComment productComment = new()
            {
                Name = model.ProductCommentVM.Name,
                Email = model.ProductCommentVM.Email,
                Message = model.ProductCommentVM.Message,
                AppUserId = userId,
                ProductId = (int)id
            };
            await _context.ProductComments.AddAsync(productComment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ProductDetail), new { id });
        }

        //sort
        public async Task<IActionResult> Sort(string? sortValue, int page = 1, int take = 9)
        {
            List<Product> products = new();

            if (sortValue == "1")
            {
                products = await _context.Products.Include(m => m.Images).ToListAsync();
            };
            if (sortValue == "2")
            {
                products = await _context.Products.Include(m => m.Images).OrderByDescending(p => p.SaleCount).ToListAsync();

            };
            if (sortValue == "3")
            {
                products = await _context.Products.Include(m => m.Images).OrderByDescending(p => p.Rate).ToListAsync();

            };
            if (sortValue == "4")
            {
                products = await _context.Products.Include(m => m.Images).OrderByDescending(p => p.CreateDate).ToListAsync();

            };
            if (sortValue == "5")
            {
                products = await _context.Products.Include(m => m.Images).OrderBy(p => p.Price).ToListAsync();

            };
            if (sortValue == "6")
            {
                products = await _context.Products.Include(m => m.Images).OrderByDescending(p => p.Price).ToListAsync();

            };
            //int pageCount = products.Count();
            int pageCount = await GetPageCountAsync(take, sortValue, null, null, null, null, null, null,null,null);
            List<ProductVM> mappedDatas = GetMappedDatas(products);
            Paginate<ProductVM> model = new(mappedDatas, page, pageCount);

            return PartialView("_ProductListPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int? id)
        {
            if (id is null) return BadRequest();

            Product dbProduct = await _productService.GetByIdAsync((int)id);

            if (dbProduct == null) return NotFound();

            List<CartVM> carts = _cartService.GetDatasFromCookie();

            CartVM existProduct = carts.FirstOrDefault(p => p.ProductId == id);

            _cartService.SetDatasToCookie(carts, dbProduct, existProduct);

            int cartCount = carts.Count;

            return Ok(cartCount);
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist(int? id)
        {
            if (id is null) return BadRequest();

            Product dbProduct = await _productService.GetByIdAsync((int)id);

            if (dbProduct == null) return NotFound();

            List<WishlistVM> wishlists = _wishlistService.GetDatasFromCookie();

            WishlistVM existProduct = wishlists.FirstOrDefault(p => p.ProductId == id);

            _wishlistService.SetDatasToCookie(wishlists, dbProduct, existProduct);

            int wishlistCount = wishlists.Count;

            return Ok(wishlistCount);
        }


    }
}
