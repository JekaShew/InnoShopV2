using InnoShop.CommonLibrary.Logs;
using InnoShop.CommonLibrary.Response;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Mappers;
using ProductManagement.Domain.Data.Models;
using ProductManagement.Infrastructure.Data;
using System.Reflection;

namespace ProductManagement.Infrastructure.Repositories
{
    public class CategoryRepository : ICategory
    {
        private readonly ProductManagementDBContext _pmDBContext;
        public CategoryRepository(ProductManagementDBContext pmDBContext)
        {
            _pmDBContext = pmDBContext;
        }

        public async Task<Response> AddCategory(CategoryDTO categoryDTO)
        {
            try
            {
                var category = CategoryMapper.CategoryDTOToCategory(categoryDTO);

                await _pmDBContext.Categories.AddAsync(category!);
                await _pmDBContext.SaveChangesAsync();

                return new Response(true, "Successfully Added!");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while adding new Category!");
            }
        }

        public async Task<Response> DeleteCategoryById(Guid categoryId)
        {
            try
            {
                var category = await _pmDBContext.Categories.FindAsync(categoryId);
                if (category == null)
                    return new Response(false, "Category not found!");

                _pmDBContext.Categories.Remove(category);
                await _pmDBContext.SaveChangesAsync();
                
                return new Response(true, "Successfully Deleted!");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while deleting Category!");
            }
        }

        public async Task<List<CategoryDTO>> TakeAllCategories()
        {
            try
            {
                var categoryDTOs = await _pmDBContext.Categories
                                .AsNoTracking()
                                .Select(c => CategoryMapper.CategoryToCategoryDTO(c))
                                .ToListAsync();
                return categoryDTOs;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return null;
            }
        }

        public async Task<CategoryDTO> TakeCategoryById(Guid categoryId)
        {
            try
            {
                var categoryDTO = CategoryMapper.CategoryToCategoryDTO(
                await _pmDBContext.Categories
                   .AsNoTracking()
                   .FirstOrDefaultAsync(c => c.Id == categoryId));
                return categoryDTO;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return null;
            }
        }

        public async Task<Response> UpdateCategory(CategoryDTO categoryDTO)
        {
            try
            {
                var category = await _pmDBContext.Categories.FindAsync(categoryDTO.Id);

                if (category == null)
                {
                    return new Response(false, "Category not Found!");
                }

                ApplyPropertiesFromDTOToModel(categoryDTO, category);

                _pmDBContext.Categories.Update(category);
                await _pmDBContext.SaveChangesAsync();

                return new Response(true, "Successfully Updated!");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while updating Category!");
            }
        }

        private void ApplyPropertiesFromDTOToModel(CategoryDTO categoryDTO, Category category)
        {
            var dtoProperties = categoryDTO.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var modelProperties = category.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var dtoProperty in dtoProperties)
            {
                var modelProperty = modelProperties.FirstOrDefault(p => p.Name == dtoProperty.Name && p.PropertyType == dtoProperty.PropertyType);
                if (modelProperty != null)
                {
                    modelProperty.SetValue(category, dtoProperty.GetValue(categoryDTO));
                }
            }
        }
    }
}
