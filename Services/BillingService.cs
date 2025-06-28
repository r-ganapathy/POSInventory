using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using POSInventory.DTOs;
using POSInventory.Models;
using POSInventory.Repositories;

namespace POSInventory.Services
{
    public interface IBillingService
    {
        Task<BillingResponseDto> CreateBillAsync(BillingRequestDto request);
    }

    public class BillingService : IBillingService
    {
        private readonly IInventoryRepository _inventoryRepo;
        private readonly IBillingRepository _billingRepo;
        public BillingService(IInventoryRepository inventoryRepo, IBillingRepository billingRepo)
        {
            _inventoryRepo = inventoryRepo;
            _billingRepo = billingRepo;
        }

        public async Task<BillingResponseDto> CreateBillAsync(BillingRequestDto request)
        {
            var items = new List<BillingItemDto>();
            var billItems = new List<Models.BillItem>();
            decimal subtotal = 0;
            var inventoryToUpdate = new List<Inventory>();
            foreach (var reqItem in request.Items)
            {
                var inv = await _inventoryRepo.GetByIdAsync(reqItem.InventoryId);
                if (inv == null || inv.IsDeleted) continue;
                if (inv.AvailableCount < reqItem.Quantity) continue; // Not enough stock
                inv.AvailableCount -= reqItem.Quantity;
                inventoryToUpdate.Add(inv);
                var item = new BillingItemDto
                {
                    InventoryId = inv.InventoryId,
                    Name = inv.Name,
                    Price = inv.Price,
                    Quantity = reqItem.Quantity
                };
                items.Add(item);
                subtotal += item.Total;
                billItems.Add(new Models.BillItem
                {
                    InventoryId = inv.InventoryId,
                    Name = inv.Name,
                    Price = inv.Price,
                    Quantity = reqItem.Quantity,
                    Total = item.Total
                });
            }
            foreach (var inv in inventoryToUpdate)
            {
                await _inventoryRepo.UpdateAsync(inv);
            }
            var bill = new Models.Bill
            {
                CreatedDate = System.DateTime.UtcNow,
                Subtotal = subtotal,
                Total = subtotal,
                Items = billItems
            };
            await _billingRepo.AddBillAsync(bill);
            return new BillingResponseDto
            {
                Items = items,
                Subtotal = subtotal,
                Total = subtotal
            };
        }
    }
}
