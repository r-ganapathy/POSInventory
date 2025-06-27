using Microsoft.EntityFrameworkCore;
using POSInventory.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSInventory.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetPagedAsync(int page, int pageSize, string search, string sortBy = null, bool? isDeleted = null)
        {
            var query = _context.Categories.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c => c.CategoryName.Contains(search));
            }
            if (isDeleted.HasValue)
            {
                query = query.Where(c => c.IsDeleted == isDeleted.Value);
            }
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.ToLower() == "name")
                    query = query.OrderBy(c => c.CategoryName);
                else
                    query = query.OrderBy(c => c.CategoryId);
            }
            else
            {
                query = query.OrderBy(c => c.CategoryId);
            }
            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync(string search, bool? isDeleted = null)
        {
            var query = _context.Categories.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c => c.CategoryName.Contains(search));
            }
            if (isDeleted.HasValue)
            {
                query = query.Where(c => c.IsDeleted == isDeleted.Value);
            }
            return await query.CountAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(Category category)
        {
            category.IsDeleted = true;
            category.ModifiedDate = System.DateTime.UtcNow;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
