using Microsoft.EntityFrameworkCore;
using POSInventory.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSInventory.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly AppDbContext _context;
        public InventoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Inventory>> GetPagedAsync(int page, int pageSize, string search, string sortBy = null, int? categoryId = null, bool? isDeleted = null)
        {
            var query = _context.Inventories.Include(i => i.Category).AsNoTracking();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(i => i.Name.Contains(search) || i.Category.CategoryName.Contains(search));
            }
            if (categoryId.HasValue)
            {
                query = query.Where(i => i.CategoryId == categoryId.Value);
            }
            if (isDeleted.HasValue)
            {
                query = query.Where(i => i.IsDeleted == isDeleted.Value);
            }
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.ToLower() == "name")
                    query = query.OrderBy(i => i.Name);
                else if (sortBy.ToLower() == "category")
                    query = query.OrderBy(i => i.Category.CategoryName);
                else
                    query = query.OrderBy(i => i.InventoryId);
            }
            else
            {
                query = query.OrderBy(i => i.InventoryId);
            }
            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync(string search, int? categoryId = null, bool? isDeleted = null)
        {
            var query = _context.Inventories.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(i => i.Name.Contains(search));
            }
            if (categoryId.HasValue)
            {
                query = query.Where(i => i.CategoryId == categoryId.Value);
            }
            if (isDeleted.HasValue)
            {
                query = query.Where(i => i.IsDeleted == isDeleted.Value);
            }
            return await query.CountAsync();
        }

        public async Task<Inventory> GetByIdAsync(int id)
        {
            return await _context.Inventories.Include(i => i.Category).AsNoTracking().FirstOrDefaultAsync(i => i.InventoryId == id);
        }

        public async Task AddAsync(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Inventory inventory)
        {
            _context.Inventories.Update(inventory);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(Inventory inventory)
        {
            inventory.IsDeleted = true;
            inventory.ModifiedDate = System.DateTime.UtcNow;
            _context.Inventories.Update(inventory);
            await _context.SaveChangesAsync();
        }
    }
}
