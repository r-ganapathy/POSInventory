namespace POSInventory.DTOs
{
    public class BillingItemDto
    {
        public int InventoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total => Price * Quantity;
    }

    public class BillingRequestDto
    {
        public List<BillingItemRequestDto> Items { get; set; }
    }

    public class BillingItemRequestDto
    {
        public int InventoryId { get; set; }
        public int Quantity { get; set; }
    }

    public class BillingResponseDto
    {
        public List<BillingItemDto> Items { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
    }
}
