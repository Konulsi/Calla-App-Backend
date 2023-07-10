using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CallaApp.Services
{
    public class ContactService: IContactService
    {
        private readonly AppDbContext _context;

        public ContactService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Contact>> GetAllAsync()
        {
            return await _context.Contact.ToListAsync();
        }
        public async Task<Contact> GetByIdAsync(int? id)
        {
            return await _context.Contact.FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
