using Microsoft.AspNetCore.Mvc;
using POSInventory.DTOs;
using POSInventory.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POSInventory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _service;
        public InventoryController(IInventoryService service)
        {
            _service = service;
        }

        // GET: api/inventory
        [HttpGet]
        public async Task<IActionResult> GetInventory(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] int? categoryId = null,
            [FromQuery] bool? isDeleted = null)
        {
            var (items, totalCount) = await _service.GetPagedAsync(page, pageSize, search, sortBy, categoryId, isDeleted);
            var result = new
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Items = items
            };
            return Ok(result);
        }

        // GET: api/inventory/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryDto>> GetInventoryItem(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/inventory
        [HttpPost]
        public async Task<ActionResult<InventoryDto>> CreateInventory(InventoryCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);
            if (result == null) return BadRequest("Invalid CategoryId");
            return CreatedAtAction(nameof(GetInventoryItem), new { id = result.InventoryId }, result);
        }

        // PUT: api/inventory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventory(int id, InventoryUpdateDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return BadRequest("Invalid InventoryId or CategoryId");
            return NoContent();
        }

        // DELETE: api/inventory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            var success = await _service.SoftDeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
