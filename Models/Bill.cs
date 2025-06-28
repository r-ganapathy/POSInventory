using System;
using System.Collections.Generic;

namespace POSInventory.Models
{
    public class Bill
    {
        public int BillId { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public List<BillItem> Items { get; set; }
    }

    public class BillItem
    {
        public int BillItemId { get; set; }
        public int BillId { get; set; }
        public int InventoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public Bill Bill { get; set; }
    }
}
