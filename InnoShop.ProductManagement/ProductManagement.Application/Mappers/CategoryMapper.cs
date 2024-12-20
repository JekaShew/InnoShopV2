using ProductManagement.Application.DTOs;
using ProductManagement.Domain.Data.Models;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Mappers
{ 
    [Mapper]
    public static partial class CategoryMapper
    {
        public static partial CategoryDTO? CategoryToCategoryDTO(Category? category);

        public static partial Category? CategoryDTOToCategory(CategoryDTO? categoryDTO);
    }
}
