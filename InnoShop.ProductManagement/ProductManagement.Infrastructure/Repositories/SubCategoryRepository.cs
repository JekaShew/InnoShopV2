using Azure.Core;
using InnoShop.CommonLibrary.Logs;
using InnoShop.CommonLibrary.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Mappers;
using ProductManagement.Domain.Data.Models;
using ProductManagement.Infrastructure.Data;
using System.Reflection;
using System.Threading;

namespace ProductManagement.Infrastructure.Repositories
{
    public class SubCategoryRepository : ISubCategory
    {
        private readonly ProductManagementDBContext _pmDBContext;

        public SubCategoryRepository(ProductManagementDBContext pmDBContext)
        {
            _pmDBContext = pmDBContext;
        }

        public async Task<Response> AddSubCategory(SubCategoryDTO subCategoryDTO)
        {
            try
            {
                if (!await _pmDBContext.Categories.AnyAsync(c => c.Id == subCategoryDTO.CategoryId))
                    return new Response(false, "No Categories Id matches Your SubCategory!");

                var subCategory = SubCategoryMapper.SubCategoryDTOToSubCategory(subCategoryDTO);

                await _pmDBContext.SubCategories.AddAsync(subCategory);
                await _pmDBContext.SaveChangesAsync();

                return new Response(true, "Successfully Added!");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while adding new SubCategory!");
            }
        }

        public async Task<Response> DeleteSubCategoryById(Guid subCategoryId)
        {
            try
            {
                var subCategory = await _pmDBContext.SubCategories.FindAsync(subCategoryId);
                if (subCategory == null)
                    return new Response(false, "SubCategory not found!");

                _pmDBContext.SubCategories.Remove(subCategory);
                await _pmDBContext.SaveChangesAsync();

                return new Response(true, "Successfully Deleted!");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while deleting SubCategory!");
            }
        }

        public async Task<List<SubCategoryDTO>> TakeAllSubCategories()
        {
            try
            {
                var subCategoryDTOs = 
                    await _pmDBContext.SubCategories
                            .Include(c => c.Category)
                            .AsNoTracking()
                            .Select(sc => SubCategoryMapper.SubCategoryToSubCategoryDTO(sc))
                            .ToListAsync();

                return subCategoryDTOs;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return null;
            }
        }

        public async Task<SubCategoryDTO> TakeSubCategoryById(Guid subCategoryId)
        {
            try
            {
                var subCategoryDTO = SubCategoryMapper.SubCategoryToSubCategoryDTO(
                                await _pmDBContext.SubCategories
                                            .Include(c => c.Category)
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(sc =>
                                                sc.Id == subCategoryId));
                
                return subCategoryDTO;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return null;
            }
        }

        public async Task<Response> UpdateSubCategory(SubCategoryDTO subCategoryDTO)
        {
            try
            {
                var subCategory = await _pmDBContext.SubCategories.FindAsync(subCategoryDTO.Id);

                if (subCategory == null)
                {
                    return new Response(false, "SubCategory not Found!");
                }

                ApplyPropertiesFromDTOToModel(subCategoryDTO, subCategory);

                _pmDBContext.SubCategories.Update(subCategory);
                await _pmDBContext.SaveChangesAsync();

                return new Response(true, "Successfully Updated!");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while updating SubCategory!");
            }
        }

        private void ApplyPropertiesFromDTOToModel(SubCategoryDTO subCategoryDTO, SubCategory subCategory)
        {
            var dtoProperties = subCategoryDTO.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var modelProperties = subCategory.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var dtoProperty in dtoProperties)
            {
                var modelProperty = modelProperties.FirstOrDefault(p => p.Name == dtoProperty.Name && p.PropertyType == dtoProperty.PropertyType);
                if (modelProperty != null)
                {
                    modelProperty.SetValue(subCategory, dtoProperty.GetValue(subCategoryDTO));
                }
            }
        }
    }
}
