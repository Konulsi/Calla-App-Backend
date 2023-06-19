using CallaApp.Models;

namespace CallaApp.Services.Interfaces
{
    public interface IHeaderBackgroundService
    {
        List<HeaderBackground> GetHeaderBackgroundsAsync();

        Task<HeaderBackground> GetHeaderBackgroundByIdAsync(int? id);
    }
}
