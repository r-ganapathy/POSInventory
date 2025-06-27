using POSInventory.DTOs;
using POSInventory.Models;
using POSInventory.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSInventory.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repo;
        private readonly AppDbContext _context;
        public InventoryService(IInventoryRepository repo, AppDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        public async Task<(IEnumerable<InventoryDto> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, string search, string sortBy = null, int? categoryId = null, bool? isDeleted = null)
        {
            var items = await _repo.GetPagedAsync(page, pageSize, search, sortBy, categoryId, isDeleted);
            var total = await _repo.GetCountAsync(search, categoryId, isDeleted);
            var dtos = items.Select(i => new InventoryDto
            {
                InventoryId = i.InventoryId,
                Name = i.Name,
                Price = i.Price,
                AvailableCount = i.AvailableCount,
                CategoryId = i.CategoryId,
                CategoryName = i.Category?.CategoryName
            });
            return (dtos, total);
        }

        public async Task<InventoryDto> GetByIdAsync(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return null;
            return new InventoryDto
            {
                InventoryId = item.InventoryId,
                Name = item.Name,
                Price = item.Price,
                AvailableCount = item.AvailableCount,
                CategoryId = item.CategoryId,
                CategoryName = item.Category?.CategoryName
            };
        }

        public async Task<InventoryDto> CreateAsync(InventoryCreateDto dto)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == dto.CategoryId && !c.IsDeleted);
            if (category == null) return null;
            var inventory = new Inventory
            {
                Name = dto.Name,
                Price = dto.Price,
                AvailableCount = dto.AvailableCount,
                CategoryId = dto.CategoryId,
                CreatedBy = "system",
                CreatedDate = System.DateTime.UtcNow,
                ModifiedBy = "system",
                ModifiedDate = System.DateTime.UtcNow
            };
            await _repo.AddAsync(inventory);
            return new InventoryDto
            {
                InventoryId = inventory.InventoryId,
                Name = inventory.Name,
                Price = inventory.Price,
                AvailableCount = inventory.AvailableCount,
                CategoryId = inventory.CategoryId,
                CategoryName = category.CategoryName
            };
        }

        public async Task<bool> UpdateAsync(int id, InventoryUpdateDto dto)
        {
            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null) return false;
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == dto.CategoryId && !c.IsDeleted);
            if (category == null) return false;
            inventory.Name = dto.Name;
            inventory.Price = dto.Price;
            inventory.AvailableCount = dto.AvailableCount;
            inventory.CategoryId = dto.CategoryId;
            inventory.ModifiedBy = "system";
            inventory.ModifiedDate = System.DateTime.UtcNow;
            await _repo.UpdateAsync(inventory);
            return true;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null) return false;
            await _repo.SoftDeleteAsync(inventory);
            return true;
        }
    }
}
