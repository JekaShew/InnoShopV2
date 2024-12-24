using InnoShop.CommonLibrary.CommonDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
