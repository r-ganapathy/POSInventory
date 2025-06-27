using System;

namespace POSInventory.Models
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int AvailableCount { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        // Navigation property
        public Category Category { get; set; }
    }
}
