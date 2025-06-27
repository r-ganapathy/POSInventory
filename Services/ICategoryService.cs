using POSInventory.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POSInventory.Services
{
    public interface ICategoryService
    {
        Task<(IEnumerable<CategoryDto> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, string search, string sortBy = null, bool? isDeleted = null);
        Task<CategoryDto> GetByIdAsync(int id);
        Task<CategoryDto> CreateAsync(CategoryCreateDto dto);
        Task<bool> UpdateAsync(int id, CategoryUpdateDto dto);
        Task<bool> SoftDeleteAsync(int id);
    }
}
