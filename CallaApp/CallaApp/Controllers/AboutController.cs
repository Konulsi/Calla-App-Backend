using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using CallaApp.ViewModels.About;
using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAboutService _aboutService;
        private readonly ITeamService _teamService;
        public AboutController(IAboutService aboutService,
                              ITeamService teamService,
                              AppDbContext context)
        {
            _aboutService = aboutService;
            _teamService = teamService;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<About> abouts = await _aboutService.GetAllAsync();
            List<Team> teams = await _teamService.GetAllAsync();
            Dictionary<string, string> headerBackgrounds = _context.HeaderBackgrounds.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);

            AboutVM model = new()
            {
                HeaderBackgrounds = headerBackgrounds,
                Abouts = abouts,
                Teams = teams
            };

            return View(model);
        }
    }
}
