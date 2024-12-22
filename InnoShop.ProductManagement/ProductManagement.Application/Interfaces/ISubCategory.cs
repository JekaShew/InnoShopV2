using InnoShop.CommonLibrary.Response;
using ProductManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Interfaces
{
    public interface ISubCategory
    {
        public Task<Response> AddSubCategory(SubCategoryDTO subCategoryDTO);
        public Task<List<SubCategoryDTO>> TakeAllSubCategories();
        public Task<SubCategoryDTO> TakeSubCategoryById(Guid subCategoryId);
        public Task<Response> UpdateSubCategory(SubCategoryDTO subCategoryDTO);
        public Task<Response> DeleteSubCategoryById(Guid subCategoryId);
    }
}
