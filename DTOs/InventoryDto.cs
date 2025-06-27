namespace POSInventory.DTOs
{
    public class InventoryDto
    {
        public int InventoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int AvailableCount { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class InventoryCreateDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int AvailableCount { get; set; }
        public int CategoryId { get; set; }
    }

    public class InventoryUpdateDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int AvailableCount { get; set; }
        public int CategoryId { get; set; }
    }
}
