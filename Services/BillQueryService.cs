using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using POSInventory.DTOs;
using POSInventory.Repositories;
using POSInventory.Models;

namespace POSInventory.Services
{
    public interface IBillQueryService
    {
        Task<BillListResponseDto> GetPagedAsync(BillListRequestDto req);
    }

    public class BillQueryService : IBillQueryService
    {
        private readonly IBillingRepository _repo;
        public BillQueryService(IBillingRepository repo)
        {
            _repo = repo;
        }
        public async Task<BillListResponseDto> GetPagedAsync(BillListRequestDto req)
        {
            var (bills, total) = await _repo.GetPagedAsync(req);
            var dtos = bills.Select(b => new BillDto
            {
                BillId = b.BillId,
                CreatedDate = b.CreatedDate,
                Subtotal = b.Subtotal,
                Total = b.Total,
                Items = b.Items.Select(i => new BillItemDto
                {
                    BillItemId = i.BillItemId,
                    InventoryId = i.InventoryId,
                    Name = i.Name,
                    Price = i.Price,
                    Quantity = i.Quantity,
                    Total = i.Total
                }).ToList()
            }).ToList();
            return new BillListResponseDto
            {
                Items = dtos,
                TotalCount = total
            };
        }
    }
}
