using System;
using System.Collections.Generic;

namespace POSInventory.DTOs
{
    public class BillDto
    {
        public int BillId { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public List<BillItemDto> Items { get; set; }
    }

    public class BillItemDto
    {
        public int BillItemId { get; set; }
        public int InventoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }

    public class BillListRequestDto
    {
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
        public string Search { get; set; }
        public string SortBy { get; set; }
        public bool? SortDesc { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class BillListResponseDto
    {
        public List<BillDto> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
