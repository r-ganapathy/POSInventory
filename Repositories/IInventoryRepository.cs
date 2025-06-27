using POSInventory.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POSInventory.Repositories
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> GetPagedAsync(int page, int pageSize, string search, string sortBy = null, int? categoryId = null, bool? isDeleted = null);
        Task<int> GetCountAsync(string search, int? categoryId = null, bool? isDeleted = null);
        Task<Inventory> GetByIdAsync(int id);
        Task AddAsync(Inventory inventory);
        Task UpdateAsync(Inventory inventory);
        Task SoftDeleteAsync(Inventory inventory);
    }
}
