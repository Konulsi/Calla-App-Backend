using CallaApp.Models;

namespace CallaApp.Services.Interfaces
{
    public interface ITeamService
    {
        //Task<Team> GetByIdAsync(int? id);
        Task<List<Team>> GetAllAsync();
        Task<Team> GetFullDataByIdAsync(int? id);
    }
}
