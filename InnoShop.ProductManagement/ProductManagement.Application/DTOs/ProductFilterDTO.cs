using InnoShop.CommonLibrary.CommonDTOs;

namespace ProductManagement.Application.DTOs
{
    public class ProductFilterDTO
    {
        public List<ParameterDTO>? SubCategory { get; set; }
        public List<ParameterDTO>? Category { get; set; }  
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        

    }
}
