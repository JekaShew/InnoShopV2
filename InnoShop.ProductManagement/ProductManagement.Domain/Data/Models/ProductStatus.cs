namespace ProductManagement.Domain.Data.Models
{
    public class ProductStatus
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        public List<Product>? Products { get; set; }
    }
}
