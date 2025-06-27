using POSInventory.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POSInventory.Services
{
    public interface IInventoryService
    {
        Task<(IEnumerable<InventoryDto> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, string search, string sortBy = null, int? categoryId = null, bool? isDeleted = null);
        Task<InventoryDto> GetByIdAsync(int id);
        Task<InventoryDto> CreateAsync(InventoryCreateDto dto);
        Task<bool> UpdateAsync(int id, InventoryUpdateDto dto);
        Task<bool> SoftDeleteAsync(int id);
    }
}
