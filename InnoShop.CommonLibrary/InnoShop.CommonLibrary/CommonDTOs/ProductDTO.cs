using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace InnoShop.CommonLibrary.CommonDTOs
{
    public class ProductDTO
    {
        public Guid? Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required, Range(0.01, Double.MaxValue), DefaultValue(0.01)]
        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public string? Description { get; set; }
        public Guid? UserId { get; set; }
        public ParameterDTO? User { get; set; }
        [Required]
        public Guid ProductStatusId { get; set; }
        public ParameterDTO? ProductStatus { get; set; }
        public Guid SubCategoryId { get; set; }
        public ParameterDTO? SubCategory { get; set; }
    }
}
