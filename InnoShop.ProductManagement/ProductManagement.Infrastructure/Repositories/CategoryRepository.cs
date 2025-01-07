using InnoShop.CommonLibrary.Logs;
using InnoShop.CommonLibrary.Response;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Mappers;
using ProductManagement.Infrastructure.Data;

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

                await _pmDBContext.AddAsync(category!);
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
                var category = CategoryMapper.CategoryDTOToCategory(categoryDTO);
                _pmDBContext.Categories.Update(category);
                await _pmDBContext.SaveChangesAsync();

                return new Response(true, "Successfully Updated!");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while updating Category !");
            }
        }
    }
}
