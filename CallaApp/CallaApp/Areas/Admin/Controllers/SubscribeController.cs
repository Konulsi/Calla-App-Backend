using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services;
using CallaApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubscribeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISubscribeService _subscribeService;
        public SubscribeController(AppDbContext context, ISubscribeService subscribeService)
        {
            _context = context;
            _subscribeService = subscribeService;
        }
        public async Task<IActionResult> Index()
        {
            var subscribes = await _subscribeService.GetAllAsync();
            return View(subscribes);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Subscribe dbSubscribe = await _subscribeService.GetByIdAsync((int)id);
                if (dbSubscribe is null) return NotFound();

                _context.Subscribes.Remove(dbSubscribe);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }
    }
}
