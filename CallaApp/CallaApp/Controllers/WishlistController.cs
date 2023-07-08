using CallaApp.Models;
using CallaApp.Services;
using CallaApp.Services.Interfaces;
using CallaApp.ViewModels.Wishlist;
using Microsoft.AspNetCore.Mvc;
namespace CallaApp.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IProductService _productService;
        private readonly IWishlistService _wishlistService;
        public WishlistController(IProductService productService, IWishlistService wishlistService)
        {
            _productService = productService;
            _wishlistService = wishlistService;
        }
        public async Task<IActionResult> Index()
        {
            List<WishlistVM> wishlists = _wishlistService.GetDatasFromCookie();
            List<WishlistDetailVM> wishlistDetailVMs = new();
            foreach (var item in wishlists)
            {
                Product dbProduct = await _productService.GetByIdAsync((int)item.ProductId);

                wishlistDetailVMs.Add(new WishlistDetailVM()
                {
                    Id = dbProduct.Id,
                    Name = dbProduct.Name,
                    Price = dbProduct.Price,
                    Image = dbProduct.Images.FirstOrDefault(m => m.IsMain).Image,
                });
            }
            return View(wishlistDetailVMs);
        }


        [HttpPost]
        public IActionResult DeleteDataFromWishlist(int? id)
        {
            if (id is null) return BadRequest();

            _wishlistService.DeleteData((int)id);
            List<WishlistVM> wishlists = _wishlistService.GetDatasFromCookie();

            return Ok(wishlists.Count);

        }
    }
}
