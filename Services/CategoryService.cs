using POSInventory.DTOs;
using POSInventory.Models;
using POSInventory.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSInventory.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        private readonly AppDbContext _context;
        public CategoryService(ICategoryRepository repo, AppDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        public async Task<(IEnumerable<CategoryDto> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, string search, string sortBy = null, bool? isDeleted = null)
        {
            var items = await _repo.GetPagedAsync(page, pageSize, search, sortBy, isDeleted);
            var total = await _repo.GetCountAsync(search, isDeleted);
            var dtos = items.Select(c => new CategoryDto
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            });
            return (dtos, total);
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return null;
            return new CategoryDto
            {
                CategoryId = item.CategoryId,
                CategoryName = item.CategoryName
            };
        }

        public async Task<CategoryDto> CreateAsync(CategoryCreateDto dto)
        {
            var category = new Category
            {
                CategoryName = dto.CategoryName,
                CreatedBy = "system",
                CreatedDate = System.DateTime.UtcNow,
                ModifiedBy = "system",
                ModifiedDate = System.DateTime.UtcNow
            };
            await _repo.AddAsync(category);
            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };
        }

        public async Task<bool> UpdateAsync(int id, CategoryUpdateDto dto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;
            category.CategoryName = dto.CategoryName;
            category.ModifiedBy = "system";
            category.ModifiedDate = System.DateTime.UtcNow;
            await _repo.UpdateAsync(category);
            return true;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;
            await _repo.SoftDeleteAsync(category);
            return true;
        }
    }
}
