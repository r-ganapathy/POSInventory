using System;
using System.Collections.Generic;

namespace POSInventory.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        // Navigation property
        public ICollection<Inventory> Inventories { get; set; }
    }
}
