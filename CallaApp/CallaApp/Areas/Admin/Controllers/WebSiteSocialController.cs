using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services;
using CallaApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WebSiteSocialController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IWebSiteSocialService _webSiteSocialService;

        public WebSiteSocialController(IWebHostEnvironment env,
                        IWebSiteSocialService webSiteSocialService,
                        AppDbContext context)
        {
            _env = env;
            _context = context;
            _webSiteSocialService = webSiteSocialService;
        }
        public async Task<IActionResult> Index()
        {
            List<WebSiteSocial> webSiteSocials = await _webSiteSocialService.GetAllAsync();
            return View(webSiteSocials);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            WebSiteSocial dbSocial = await _webSiteSocialService.GetByIdAsync(id);

            WebSiteSocial model = new()
            {
                Icon = dbSocial.Icon,
                Link = dbSocial.Link,
            };

            return View(model);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, WebSiteSocial updatedSocial)
        {
            try
            {
                if (id == null) return BadRequest();
                WebSiteSocial dbSocial = await _webSiteSocialService.GetByIdAsync(id);
                if (dbSocial is null) return NotFound();

                WebSiteSocial model = new()
                {
                    Icon = dbSocial.Icon,
                    Link = dbSocial.Link,
                };

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                dbSocial.Icon = updatedSocial.Icon;
                dbSocial.Link = updatedSocial.Link;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                @ViewBag.error = ex.Message;
                return View();
            }
        }
    }
}
