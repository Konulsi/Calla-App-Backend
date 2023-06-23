using CallaApp.Data;
using CallaApp.Models;
using CallaApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CallaApp.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetAllAsync() => await _context.Categories.Include(m => m.ProductCategories).ToListAsync();
        public async Task<Category> GetByIdAsync(int? id) => await _context.Categories.Include(m => m.ProductCategories).FirstOrDefaultAsync(m => m.Id == id);
    }
}
