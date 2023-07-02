using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using CallaApp.ViewModels.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace CallaApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBrandService _brandService;
        private readonly IWebSiteSocialService _webSiteSocialService;
        public ContactController(AppDbContext context, 
                                IBrandService brandService,
                                IWebSiteSocialService webSiteSocialService)
        {
            _context = context;
            _brandService = brandService;
            _webSiteSocialService = webSiteSocialService;
        }
        public async Task<IActionResult> Index()
        {
            List<Brand> brands = await _brandService.GetAllAsync();
            List<WebSiteSocial> socials = await _webSiteSocialService.GetAllAsync();
            Dictionary<string, string> headerBackgrounds = _context.HeaderBackgrounds.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);
            Dictionary<string, string> settings = _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);

            ContactVM model = new()
            {
                Brands = brands,
                Socials = socials,
                Settings = settings,
                HeaderBackgrounds = headerBackgrounds
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> PostComment(ContactVM model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", model);
            Contact contact = new()
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Message = model.Message,
            };

            await _context.Contact.AddAsync(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
