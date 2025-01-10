namespace ProductManagement.Domain.Data.Models
{
    public class SubCategory
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        public List<Product>? Products { get; set; }
    }
}
