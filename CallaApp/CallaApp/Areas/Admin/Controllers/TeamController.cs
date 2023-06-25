using CallaApp.Areas.Admin.ViewModels.Advertising;
using CallaApp.Areas.Admin.ViewModels.Team;
using CallaApp.Data;
using CallaApp.Helpers;
using CallaApp.Models;
using CallaApp.Services;
using CallaApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CallaApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ITeamService _teamService;
        //private readonly ITeamSocialService _teamSocialService;
        private readonly IPositionService _positionService;
        private readonly IWebHostEnvironment _env;
        public TeamController(AppDbContext context,
                              ITeamService teamService,
                              IWebHostEnvironment env,
                              IPositionService positionService
                              /*ITeamSocialService teamSocialService*/)
        {
            _context = context;
            _teamService = teamService;
            _env = env;
            _positionService = positionService;
            //_teamSocialService = teamSocialService;
        }

        public async Task<IActionResult> Index()
        {
            List<Team> teams = await _teamService.GetAllAsync();
            return View(teams);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            Team dbTeam = await _teamService.GetFullDataByIdAsync(id);
            if (dbTeam is null) return NotFound();

            return View(dbTeam);
        }


        private async Task<SelectList> GetPositionAsync()
        {
            List<Position> positions = await _positionService.GetAllAsync();
            return new SelectList(positions, "Id", "Name");
        }
        //private async Task<SelectList> GetIconAsync()
        //{
        //    List<TeamSocial> socialIcon = await _teamSocialService.GetAllAsync();
        //    return new SelectList(socialIcon, "Id", "Name");
        //}

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.positions = await GetPositionAsync();
            //ViewBag.socialIcon = await GetIconAsync();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamCreateVM createTeam)
        {
            try
            {
                ViewBag.positions = await GetPositionAsync();
                //ViewBag.socialIcon = await GetIconAsync();

                if (!ModelState.IsValid)
                {
                    return View(createTeam);
                }

                if (!createTeam.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(createTeam);
                }

                if (!createTeam.Photo.CheckFileSize(600))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 600kb");
                    return View(createTeam);
                }

                string fileName = Guid.NewGuid().ToString() + "_" + createTeam.Photo.FileName;
                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);
                await FileHelper.SaveFileAsync(path, createTeam.Photo);

                Team newTeam = new()
                {
                    Image = fileName,
                    Name = createTeam.Name,
                    Testimontial = createTeam.Testimonial,
                   
                };
                await _context.Teams.AddAsync(newTeam);

                await _context.SaveChangesAsync();

                //foreach (var item in createTeam.Icons)
                //{
                //    TeamSocial newSocial = new()
                //    {
                //        //TeamId = newTeam.Id,
                //        //Link = item.Link,
                //        Icon = item.Icon,
                //    };
                //    await _context.TeamSocials.AddAsync(newSocial);

                //    await _context.SaveChangesAsync();
                //}

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

    }
}
