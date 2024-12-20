using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Data.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductStatusId { get; set; }
        public ProductStatus ProductStatus { get; set; }
        public Guid SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }

    }
}
