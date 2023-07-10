using CallaApp.Models;

namespace CallaApp.Services.Interfaces
{
    public interface IContactService
    {
        Task<List<Contact>> GetAllAsync();
        Task<Contact> GetByIdAsync(int? id);
    }
}
