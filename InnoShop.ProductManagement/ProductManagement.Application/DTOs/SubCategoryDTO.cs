using InnoShop.CommonLibrary.CommonDTOs;
using System.ComponentModel.DataAnnotations;


namespace ProductManagement.Application.DTOs
{
    public class SubCategoryDTO
    {
        public Guid? Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        public ParameterDTO? Category { get; set; }

        //public List<ProductDTO> Products { get; set; }

    }
}
