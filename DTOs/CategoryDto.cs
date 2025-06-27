namespace POSInventory.DTOs
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class CategoryCreateDto
    {
        public string CategoryName { get; set; }
    }

    public class CategoryUpdateDto
    {
        public string CategoryName { get; set; }
    }
}
