using InnoShop.CommonLibrary.CommonDTOs;
using System.ComponentModel.DataAnnotations;


namespace ProductManagement.Application.DTOs
{
    public class SubCategoryDTO
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
        public ParameterDTO? Category { get; set; }

    }
}
