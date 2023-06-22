using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILayoutService _layoutService;

        public SettingController(IWebHostEnvironment env,
                        ILayoutService layoutService)
        {
            _env = env;
            _layoutService = layoutService;
        }
        public async Task<IActionResult> Index()
        {
            List<Settings> settings = _layoutService.GetSettingDatas();
            return View(settings);
        }
    }
}
