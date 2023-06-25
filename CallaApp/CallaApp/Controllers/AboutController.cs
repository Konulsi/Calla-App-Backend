using CallaApp.Models;
using CallaApp.Services.Interfaces;
using CallaApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CallaApp.Controllers
{
    public class AboutController : Controller
    {
        private readonly ILayoutService _layoutService;
        private readonly IAboutService _aboutService;
        private readonly ITeamService _teamService;
        public AboutController(ILayoutService layoutService,
                              IAboutService aboutService,
                              ITeamService teamService)
        {
            _layoutService = layoutService;
            _aboutService = aboutService;
            _teamService = teamService;
        }
        public async Task<IActionResult> Index()
        {
            List<HeaderBackground> headerBackgrounds = _layoutService.GetAllAsync();
            List<About> abouts = await _aboutService.GetAllAsync();
            List<Team> teams = await _teamService.GetAllAsync();

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
