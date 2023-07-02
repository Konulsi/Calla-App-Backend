using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using CallaApp.ViewModels.Wishlist;
using Microsoft.AspNetCore.Mvc;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace CallaApp.Controllers
{
    public class WishlistController : Controller
    {
        private readonly AppDbContext _context;
        public WishlistController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            Dictionary<string, string> headerBackgrounds = _context.HeaderBackgrounds.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);

            WishlistVM model = new()
            {
                HeaderBackgrounds = headerBackgrounds,
            };
            return View(model);
        }
    }
}
