using InnoShop.CommonLibrary.CommonDTOs;
using ProductManagement.Domain.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.DTOs
{
    public class ProductStatusDTO
    {
        public Guid? Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }

        //public List<ProductDTO> Products { get; set; }
    }
}
