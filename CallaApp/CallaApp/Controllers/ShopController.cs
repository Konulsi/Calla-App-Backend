using CallaApp.Data;
using CallaApp.Helpers;
using CallaApp.Models;
using CallaApp.Services;
using CallaApp.Services.Interfaces;
using CallaApp.ViewModels.Product;
using CallaApp.ViewModels.Shop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public ShopController(ITagService tagService,
                              ISizeService sizeService,
                              ICategoryService categoryService,
                              IColorService colorService,
                              IBrandService brandService,
                              AppDbContext context,
                              IProductService productService,
                              ILayoutService layoutService)
        {
            _tagService = tagService;   
            _sizeService = sizeService;
            _categoryService = categoryService;
            _colorService = colorService;
            _brandService = brandService;
            _context = context;
            _productService = productService;
            _layoutService = layoutService;
        }

        public async Task<IActionResult> Index(int page = 1, int take = 6, int? categoryId = null, int? colorId = null, int? tagId = null, int? sizeId = null, int? brandId = null)
        {
            List<Product> datas = await _productService.GetPaginatedDatasAsync(page, take, categoryId, colorId, tagId,sizeId,brandId);
            List<ProductVM> mappedDatas = GetDatas(datas);
            int pageCount = 0;
            ViewBag.catId = categoryId;
            ViewBag.tagId = tagId;
            ViewBag.sizeId = sizeId;
            ViewBag.colorId = colorId;
            ViewBag.brandId = brandId;

            List<Tag> tags = await _tagService.GetAllAsync();
            List<Size> sizes = await _sizeService.GetAllAsync();
            List<Color> colors = await _colorService.GetAllAsync();
            List<Brand> brands = await _brandService.GetAllAsync();
            List<Category> categories = await _categoryService.GetAllAsync();
            Dictionary<string, string> headerBackgrounds = _context.HeaderBackgrounds.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);

            if (categoryId != null)
            {
                pageCount = await GetPageCountAsync(take, categoryId, null, null,null,null);
            }
            if (colorId != null)
            {
                pageCount = await GetPageCountAsync(take, null, colorId, null, null, null);
            }
            if (tagId != null)
            {
                pageCount = await GetPageCountAsync(take, null, null, tagId, null, null);
            }
            if (sizeId != null)
            {
                pageCount = await GetPageCountAsync(take, null, null,  null, sizeId, null);
            }
            if (brandId != null)
            {
                pageCount = await GetPageCountAsync(take, null, null, null, null, brandId);
            }

            if (categoryId == null &&  colorId == null && tagId == null && sizeId == null &&  brandId == null)
            {
                pageCount = await GetPageCountAsync(take, null, null, null, null, null);
            }

            Paginate<ProductVM> paginatedDatas = new(mappedDatas, page, pageCount);

            ShopVM model = new()
            {
                Products = await _productService.GetAllAsync(),
                Tags = tags,
                Sizes = sizes,
                Categories = categories,
                Colors = colors,
                Brands = brands,
                HeaderBackgrounds = headerBackgrounds,
                PaginateDatas = paginatedDatas
            };
            return View(model);
        }

        private async Task<int> GetPageCountAsync(int take, int? catId, int? colorId, int? tagId, int? sizeId, int? brandId)
        {
            int prodCount = 0;
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
            if (catId == null && tagId == null && colorId == null && sizeId == null && brandId == null)
            {
                prodCount = await _productService.GetCountAsync();
            }

            return (int)Math.Ceiling((decimal)prodCount / take);
        }

        private List<ProductVM> GetDatas(List<Product> products)
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
                    //Rating = product.Rate
                };
                mappedDatas.Add(productList);
            }
            return mappedDatas;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsByCategory(int? id, int page = 1, int take = 6)
        {
            if (id is null) return BadRequest();
            ViewBag.catId = id;

            var products = await _productService.GetProductsByCategoryIdAsync(id, page, take);

            int pageCount = await GetPageCountAsync(take, (int)id, null, null,null,null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductListPartial", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByColor(int? id, int page = 1, int take = 6)
        {
            if (id is null) return BadRequest();
            ViewBag.colorId = id;
            var products = await _productService.GetProductsByColorIdAsync(id);
            int pageCount = await GetPageCountAsync(take, null, (int)id, null,null,null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductListPartial", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByTag(int? id, int page = 1, int take = 6)
        {
            if (id is null) return BadRequest();
            ViewBag.tagId = id;

            var products = await _productService.GetProductsByTagIdAsync(id);

            int pageCount = await GetPageCountAsync(take,  null, null, (int)id, null,null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductListPartial", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsBySize(int? id, int page = 1, int take = 6)
        {
            if (id is null) return BadRequest();
            ViewBag.sizeId = id;

            var products = await _productService.GetProductsBySizeIdAsync(id);

            int pageCount = await GetPageCountAsync(take, null, null, null, (int)id, null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductListPartial", model);
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsByBrand(int? id, int page = 1, int take = 6)
        {
            if (id is null) return BadRequest();
            ViewBag.brandId = id;

            var products = await _productService.GetProductsByBrandIdAsync(id);

            int pageCount = await GetPageCountAsync(take,  null, null,  null, null, (int)id);

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
                    ProductComments = dbProduct.ProductComments
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

            return RedirectToAction(nameof(ProductDetailVM), new { id });
        }

        //[HttpPost]
        //public async Task<IActionResult> Filter(string value)
        //{
        //    if (value is null) return BadRequest();
        //    var products = await _productService.GetAllAsync();
        //    switch (value)
        //    {
        //        case "Sort by Default":
        //            products = products;
        //            break;
        //        case "Sort by Popularity":
        //            products = products.OrderByDescending(p => p.SaleCount);
        //            break;
        //        case "Sort by Rated":
        //            products = products.OrderByDescending(p => p.Rating);
        //            break;
        //        case "Sort by Latest":
        //            products = products.OrderByDescending(p => p.CreatedDate);
        //            break;
        //        case "Sort by High Price":
        //            products = products.OrderByDescending(p => p.Price);
        //            break;
        //        case "Sort by Low Price":
        //            products = products.OrderBy(p => p.Price);
        //            break;
        //        default:
        //            break;
        //    }
        //    return PartialView("_ProductListPartial", products);

        //}

        //[HttpPost]
        //public async Task<IActionResult> AddToCart(int? id)
        //{
        //    if (id is null) return BadRequest();

        //    Product dbProduct = await _productService.GetByIdAsync((int)id);

        //    if (dbProduct == null) return NotFound();

        //    List<CartVM> carts = _cartService.GetDatasFromCookie();

        //    CartVM existProduct = carts.FirstOrDefault(p => p.ProductId == id);

        //    _cartService.SetDatasToCookie(carts, dbProduct, existProduct);

        //    int cartCount = carts.Count;

        //    return Ok(cartCount);
        //}

        //public async Task<IActionResult> Search(string searchText)
        //{
        //    if (string.IsNullOrEmpty(searchText))
        //    {
        //        return Ok();
        //    }
        //    var products = await _productService.GetAllBySearchText(searchText);

        //    return View(products);
        //}

    }
}
