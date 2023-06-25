//using CallaApp.Data;
//using CallaApp.Models;
//using CallaApp.Services.Interfaces;
//using Microsoft.EntityFrameworkCore;

//namespace CallaApp.Services
//{
//    public class TeamSocialService: ITeamSocialService
//    {
//        private readonly AppDbContext _context;
//        public TeamSocialService(AppDbContext context)
//        {
//            _context = context;
//        }
//        public async Task<List<TeamSocial>> GetAllAsync() => await _context.TeamSocials
//                                                                     //.Include(t => t.Team)
//                                                                     .ToListAsync();
//        public async Task<TeamSocial> GetByIdAsync(int? id) => await _context.TeamSocials.FindAsync(id);

//    }
//}
