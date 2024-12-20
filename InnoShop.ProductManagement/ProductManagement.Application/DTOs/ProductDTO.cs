using ProductManagement.Domain.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required,Range(0.01,Double.MaxValue), DefaultValue(0.01)]
        public decimal Price { get; set; }
        [DefaultValue(DateTime.UtcNow("yyy-MM-dd HH:mm:ss"))]
        public DateTime CreateDate { get; set; }
        public string Description { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid ProductStatusId { get; set; }
        public ParameterDTO ProductStatus { get; set; }
        public Guid SubCategoryId { get; set; }
        public ParameterDTO SubCategory { get; set; }
    }
}
