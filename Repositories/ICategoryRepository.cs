using POSInventory.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POSInventory.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetPagedAsync(int page, int pageSize, string search, string sortBy = null, bool? isDeleted = null);
        Task<int> GetCountAsync(string search, bool? isDeleted = null);
        Task<Category> GetByIdAsync(int id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task SoftDeleteAsync(Category category);
    }
}
