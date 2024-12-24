using ProductManagement.Application.DTOs;
using ProductManagement.Domain.Data.Models;
using Riok.Mapperly.Abstractions;

namespace ProductManagement.Application.Mappers
{
    [Mapper]
    public static partial class SubCategoryMapper
    {
        [MapProperty([nameof(SubCategory.Category), nameof(SubCategory.Category.Id)],
            [nameof(SubCategoryDTO.Category), nameof(SubCategoryDTO.Category.Id)])]
        [MapProperty([nameof(SubCategory.Category), nameof(SubCategory.Category.Title)],
            [nameof(SubCategoryDTO.Category), nameof(SubCategoryDTO.Category.Text)])]
        public static partial SubCategoryDTO? SubCategoryToSubCategoryDTO(SubCategory? subCategory);

        public static partial SubCategory? SubCategoryDTOToSubCategory(SubCategoryDTO? subCategoryDTO);
    }
}
