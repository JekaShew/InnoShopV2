using InnoShop.CommonLibrary.Response;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Interfaces
{
    public interface ICategory
    {
        public Task<Response> AddCategory(CategoryDTO categoryDTO);
        public Task<List<CategoryDTO>> TakeAllCategories();
        public Task<CategoryDTO> TakeCategoryById(Guid categoryId);
        public Task<Response> UpdateCategory(CategoryDTO categoryDTO);
        public Task<Response> DeleteCategoryById(Guid categoryId);
    }
}
