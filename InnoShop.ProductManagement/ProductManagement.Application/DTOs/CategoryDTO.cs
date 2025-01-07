using ProductManagement.Domain.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.DTOs
{
    public class CategoryDTO
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}
