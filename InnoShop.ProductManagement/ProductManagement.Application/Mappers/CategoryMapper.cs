using ProductManagement.Application.DTOs;
using ProductManagement.Domain.Data.Models;
using Riok.Mapperly.Abstractions;

namespace ProductManagement.Application.Mappers
{
    [Mapper]
    public static partial class CategoryMapper
    {
        public static partial CategoryDTO? CategoryToCategoryDTO(Category? category);
        public static partial Category? CategoryDTOToCategory(CategoryDTO? categoryDTO);
    }
}
