using CallaApp.Areas.Admin.ViewModels.Product;
using CallaApp.Data;
using CallaApp.Helpers;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CallaApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IColorService _colorService;
        private readonly ISizeService _sizeService;
        private readonly ITagService _tagService;
        private readonly IBrandService _brandService;
        public ProductController(AppDbContext context,
                        IWebHostEnvironment env,
                        IProductService productService,
                        ICategoryService categoryService,
                        ISizeService sizeService,
                        ITagService tagService,
                        IColorService colorService,
                        IBrandService brandService)
        {
            _context = context;
            _env = env;
            _productService = productService;
            _categoryService = categoryService;
            _sizeService = sizeService;
            _tagService = tagService;
            _colorService = colorService;
            _brandService = brandService;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 5)
        {
            List<Product> products = await _productService.GetPaginatedDatasAsync(page, take, null, null, null, null, null, null, null, null, null);

            List<ProductListVM> mappedDatas = GetMappedDatas(products);
            int pageCount = await GetPageCountAsync(take);

            Paginate<ProductListVM> paginatedDatas = new(mappedDatas, page, pageCount);

            ViewBag.take = take;

            return View(paginatedDatas);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            var productCount = await _productService.GetCountAsync();
            return (int)Math.Ceiling((decimal)productCount / take);
        }

        private List<ProductListVM> GetMappedDatas(List<Product> products)
        {
            List<ProductListVM> mappedDatas = new();

            foreach (var product in products)
            {
                ProductListVM productVM = new()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Images = product.Images,
                    SKU = product.SKU,
                };

                mappedDatas.Add(productVM);
            }

            return mappedDatas;
        }


        private async Task<SelectList> GetCategoryAsync()
        {
            List<Category> categories = await _categoryService.GetAllAsync();
            return new SelectList(categories, "Id", "Name");
        }

        private async Task<SelectList> GetColorAsync()
        {
            List<Color> colors = await _colorService.GetAllAsync();
            return new SelectList(colors, "Id", "Name");
        }

        private async Task<SelectList> GetSizeAsync()
        {
            List<Size> sizes = await _sizeService.GetAllAsync();
            return new SelectList(sizes, "Id", "Name");
        }

        private async Task<SelectList> GetTagAsync()
        {
            List<Tag> tags = await _tagService.GetAllAsync();
            return new SelectList(tags, "Id", "Name");
        }

        private async Task<SelectList> GetBrandAsync()
        {
            List<Brand> brands = await _brandService.GetAllAsync();
            return new SelectList(brands, "Id", "Name");
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            try
            {
                if (id == null) return BadRequest();

                Product dbProduct = await _productService.GetFullDataByIdAsync((int)id);

                if (dbProduct == null) return NotFound();


                ProductDetailVM model = new()
                {
                    Name = dbProduct.Name,
                    SaleCount = dbProduct.SaleCount,
                    StockCount = dbProduct.StockCount,
                    Description = dbProduct.Description,
                    Price = dbProduct.Price,
                    SKU = dbProduct.SKU,
                    Rate = dbProduct.Rate,
                    ProductCategories = dbProduct.ProductCategories,
                    Images = dbProduct.Images,
                    ProductSizes = dbProduct.ProductSizes,
                    ProductTags = dbProduct.ProductTags,
                    ProductColors = dbProduct.ProductColors,
                    BrandName = dbProduct.Brand.Name
                };

                return View(model);
            }
            catch (Exception)
            {
                throw;
            }

        }


        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {

            ViewBag.categories = await GetCategoryAsync();
            ViewBag.tags = await GetTagAsync();
            ViewBag.sizes = await GetSizeAsync();
            ViewBag.colors = await GetColorAsync();
            ViewBag.brands = await GetBrandAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM model)
        {
            try
            {
                ViewBag.categories = await GetCategoryAsync();
                ViewBag.tags = await GetTagAsync();
                ViewBag.sizes = await GetSizeAsync();
                ViewBag.colors = await GetColorAsync();
                ViewBag.brands = await GetBrandAsync();

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                Product newProduct = new();

                //all images
                List<ProductImage> productImages = new();

                foreach (var photo in model.Photos)
                {
                    if (!photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View();
                    }

                    if (!photo.CheckFileSize(600))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 600kb");
                        return View();
                    }
                }

                foreach (var photo in model.Photos)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);
                    await FileHelper.SaveFileAsync(path, photo);

                    ProductImage productImage = new()
                    {
                        Image = fileName
                    };

                    productImages.Add(productImage);
                }
                newProduct.Images = productImages;
                newProduct.Images.FirstOrDefault().IsMain = true;


                //category
                List<ProductCategory> productCategories = new();

                if (model.CategoryIds.Count > 0)
                {
                    foreach (var cateId in model.CategoryIds)
                    {
                        ProductCategory productCategory = new()
                        {
                            CategoryId = cateId
                        };
                        productCategories.Add(productCategory);
                    }
                    newProduct.ProductCategories = productCategories;
                }
                else
                {
                    ModelState.AddModelError("CategoryIds", "Don't be empty");
                    return View();
                }

                //tag
                List<ProductTag> productTags = new();

                if (model.TagIds.Count > 0)
                {
                    foreach (var tagId in model.TagIds)
                    {
                        ProductTag productTag = new()
                        {
                            TagId = tagId
                        };
                        productTags.Add(productTag);
                    }
                    newProduct.ProductTags = productTags;
                }
                else
                {
                    ModelState.AddModelError("TagIds", "Don't be empty");
                    return View();
                }

                //size
                List<ProductSize> productSizes = new();

                if (model.SizeIds.Count > 0)
                {
                    foreach (var sizeId in model.SizeIds)
                    {
                        ProductSize productSize = new()
                        {
                            SizeId = sizeId
                        };
                        productSizes.Add(productSize);
                    }
                    newProduct.ProductSizes = productSizes;
                }
                else
                {
                    ModelState.AddModelError("SizeIds", "Don't be empty");
                    return View();
                }

                //color
                List<ProductColor> productColors = new();

                if (model.ColorIds.Count > 0)
                {
                    foreach (var colorId in model.ColorIds)
                    {
                        ProductColor productColor = new()
                        {
                            ColorId = colorId
                        };
                        productColors.Add(productColor);
                    }
                    newProduct.ProductColors = productColors;
                }
                else
                {
                    ModelState.AddModelError("ColorIds", "Don't be empty");
                    return View();
                }

                var convertPrice = decimal.Parse(model.Price);
                Random random = new();

                newProduct.Name = model.Name;
                newProduct.Description = model.Description;
                newProduct.Price = convertPrice;
                newProduct.StockCount = model.StockCount;
                newProduct.BrandId = model.BrandId;
                newProduct.SKU = model.Name.Substring(0, 3) + "_" + random.Next();

                await _context.ProductImages.AddRangeAsync(productImages);
                await _context.Products.AddAsync(newProduct);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }


         [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                ViewBag.categories = await GetCategoryAsync();
                ViewBag.tags = await GetTagAsync();
                ViewBag.sizes = await GetSizeAsync();
                ViewBag.colors = await GetColorAsync();
                ViewBag.brands = await GetBrandAsync();

                Product dbProduct = await _productService.GetFullDataByIdAsync((int)id);
                if (dbProduct == null) return NotFound();

                ProductUpdateVM model = new()
                {
                    Id = dbProduct.Id,
                    Name = dbProduct.Name,
                    Description = dbProduct.Description,
                    Price = dbProduct.Price.ToString("0.#####"),
                    SKU = dbProduct.SKU,
                    Images = dbProduct.Images.ToList(),
                    BrandId = dbProduct.BrandId,
                    TagIds = dbProduct.ProductTags.Select(m => m.Tag.Id).ToList(),
                    SizeIds = dbProduct.ProductSizes.Select(m => m.Size.Id).ToList(),
                    CategoryIds = dbProduct.ProductCategories.Select(m => m.Category.Id).ToList(),
                    ColorIds = dbProduct.ProductColors.Select(m => m.Color.Id).ToList(),
                    StockCount = dbProduct.StockCount,
                };
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ProductUpdateVM updatedProduct)
        {
            try
            {
                ViewBag.categories = await GetCategoryAsync();
                ViewBag.tags = await GetTagAsync();
                ViewBag.sizes = await GetSizeAsync();
                ViewBag.colors = await GetColorAsync();
                ViewBag.brands = await GetBrandAsync();


                if (id == null) return BadRequest();
                Product dbProduct = await _productService.GetFullDataByIdAsync((int)id);
                if (dbProduct == null) return NotFound();

                ProductUpdateVM model = new()
                {
                    Id = dbProduct.Id,
                    Name = dbProduct.Name,
                    Description = dbProduct.Description,
                    Price = dbProduct.Price.ToString("0.#####"),
                    SKU = dbProduct.SKU,
                    Images = dbProduct.Images.ToList(),
                    BrandId = dbProduct.BrandId,
                    TagIds = dbProduct.ProductTags.Select(m => m.Tag.Id).ToList(),
                    SizeIds = dbProduct.ProductSizes.Select(m => m.Size.Id).ToList(),
                    CategoryIds = dbProduct.ProductCategories.Select(m => m.Category.Id).ToList(),
                    ColorIds = dbProduct.ProductColors.Select(m => m.Color.Id).ToList(),
                    StockCount = dbProduct.StockCount,
                };

                if (!ModelState.IsValid) return View(model);

                if (updatedProduct.Photos is not null)
                {
                    foreach (var photo in updatedProduct.Photos)
                    {
                        if (!photo.CheckFileType("image/"))
                        {
                            ModelState.AddModelError("Photo", "File type must be image");
                            return View(model);
                        }

                        if (!photo.CheckFileSize(600))
                        {
                            ModelState.AddModelError("Photo", "Image size must be max 600kb");
                            return View(model);
                        }
                    }

                    foreach (var item in dbProduct.Images)
                    {
                        string dbPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", item.Image);
                        FileHelper.DeleteFile(dbPath);
                    }

                    List<ProductImage> productImages = new();
                    foreach (var photo in updatedProduct.Photos)
                    {
                        string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);
                        await FileHelper.SaveFileAsync(path, photo);

                        ProductImage productImage = new()
                        {
                            Image = fileName
                        };
                        productImages.Add(productImage);
                    }
                    dbProduct.Images = productImages;
                    dbProduct.Images.FirstOrDefault().IsMain = true;

                    await _context.ProductImages.AddRangeAsync(productImages);
                }
                else
                {
                    Product newProduct = new()
                    {
                        Images = dbProduct.Images
                    };
                }

                List<ProductCategory> productCategories = new();
                if (updatedProduct.CategoryIds.Count > 0)
                {
                    foreach (var cateId in updatedProduct.CategoryIds)
                    {
                        ProductCategory productCategory = new()
                        {
                            CategoryId = cateId
                        };
                        productCategories.Add(productCategory);
                    }
                    dbProduct.ProductCategories = productCategories;
                }
                else
                {
                    ModelState.AddModelError("CategoryIds", "Don't be empty");
                    return View();
                }


                List<ProductTag> productTags = new();
                if (updatedProduct.TagIds.Count > 0)
                {
                    foreach (var tagId in updatedProduct.TagIds)
                    {
                        ProductTag productTag = new()
                        {
                            TagId = tagId
                        };
                        productTags.Add(productTag);
                    }
                    dbProduct.ProductTags = productTags;
                }
                else
                {
                    ModelState.AddModelError("TagIds", "Don't be empty");
                    return View();
                }


                List<ProductSize> productSizes = new();
                if (updatedProduct.SizeIds.Count > 0)
                {
                    foreach (var sizeId in updatedProduct.SizeIds)
                    {
                        ProductSize productSize = new()
                        {
                            SizeId = sizeId
                        };
                        productSizes.Add(productSize);
                    }
                    dbProduct.ProductSizes = productSizes;
                }
                else
                {
                    ModelState.AddModelError("TagIds", "Don't be empty");
                    return View();
                }

                List<ProductColor> productColors = new();
                if (updatedProduct.ColorIds.Count > 0)
                {
                    foreach (var colorId in updatedProduct.ColorIds)
                    {
                        ProductColor productColor = new()
                        {
                            ColorId = colorId
                        };
                        productColors.Add(productColor);
                    }
                    dbProduct.ProductColors = productColors;
                }
                else
                {
                    ModelState.AddModelError("TagIds", "Don't be empty");
                    return View();
                }

                var convertPrice = decimal.Parse(updatedProduct.Price);

                dbProduct.Name = updatedProduct.Name;
                dbProduct.Description = updatedProduct.Description;
                dbProduct.Price = convertPrice;
                dbProduct.StockCount = updatedProduct.StockCount;
                dbProduct.BrandId = updatedProduct.BrandId;
                dbProduct.SKU= updatedProduct.SKU;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }




        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Product dbProduct = await _productService.GetFullDataByIdAsync((int)id);
                if (dbProduct == null) return NotFound();

                foreach (var photo in dbProduct.Images)
                {
                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", photo.Image);
                    FileHelper.DeleteFile(path);
                }

                _context.Products.Remove(dbProduct);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        public async Task<IActionResult> DeleteProductImage(int? id)
        {
            try
            {
                if (id is null) return BadRequest();
                ProductImage image = await _productService.GetImageById((int)id);

                if (image is null) return NotFound();
                Product dbProduct = await _productService.GetProductByImageId((int)id);
                if (dbProduct is null) return NotFound();

                DeleteResponse response = new();
                response.Result = false;

                if (dbProduct.Images.Count > 1)
                {
                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", image.Image);
                    FileHelper.DeleteFile(path);
                    _productService.RemoveImage(image);

                    await _context.SaveChangesAsync();
                    response.Result = true;
                }

                dbProduct.Images.FirstOrDefault().IsMain = true;
                response.Id = dbProduct.Images.FirstOrDefault().Id;
                await _context.SaveChangesAsync();

                return Ok(response);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> SetStatus(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                ProductImage image = await _productService.GetImageById((int)id);
                if (image is null) return NotFound();

                Product dbProduct = await _productService.GetProductByImageId((int)id);
                if (dbProduct is null) return NotFound();

                if (!image.IsMain)
                {
                    image.IsMain = true;
                    await _context.SaveChangesAsync();
                }

                var images = dbProduct.Images.Where(m => m.Id != id).ToList();

                foreach (var item in images)
                {
                    if (item.IsMain)
                    {
                        item.IsMain = false;
                        await _context.SaveChangesAsync();
                    }
                }

                return Ok(image.IsMain);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

    }
}
