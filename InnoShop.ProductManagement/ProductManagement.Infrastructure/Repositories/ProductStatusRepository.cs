using InnoShop.CommonLibrary.Logs;
using InnoShop.CommonLibrary.Response;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Mappers;
using ProductManagement.Domain.Data.Models;
using ProductManagement.Infrastructure.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace ProductManagement.Infrastructure.Repositories
{
    public class ProductStatusRepository : IProductStatus
    {
        private readonly ProductManagementDBContext _pmDBContext;

        public ProductStatusRepository(ProductManagementDBContext pmDBContext)
        {
            _pmDBContext = pmDBContext;
        }

        public async Task<Response> AddProductStatus(ProductStatusDTO productStatusDTO)
        {
            try
            {
                var productStatus = ProductStatusMapper.ProductStatusDTOToProductStatus(productStatusDTO);

                await _pmDBContext.ProductStatuses.AddAsync(productStatus);
                await _pmDBContext.SaveChangesAsync();

                return new Response(true, "Successfully Added!");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while adding new Product Status!");
            }
        }

        public async Task<Response> DeleteProductStatusById(Guid productStatusId)
        {
            try
            {
                var productStatus = await _pmDBContext.ProductStatuses.FindAsync(productStatusId);
                if (productStatus == null)
                    return new Response(false, "Product Status not found!");

                _pmDBContext.ProductStatuses.Remove(productStatus);
                await _pmDBContext.SaveChangesAsync();

                return new Response(true, "Successfully Deleted!");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while deleting Product Status!");
            }
        }

        public async Task<List<ProductStatusDTO>> TakeAllProductStatuses()
        {
            try
            {
                var productStatusDTOs = await _pmDBContext.ProductStatuses
                        .AsNoTracking()
                        .Select(ps => ProductStatusMapper.ProductStatusToProductStatusDTO(ps))
                        .ToListAsync();

                return productStatusDTOs;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return null;
            }
        }

        public async Task<ProductStatusDTO> TakeProductStatusById(Guid productStatusId)
        {
            try
            {
                var productStatusDTO = ProductStatusMapper.ProductStatusToProductStatusDTO(
                    await _pmDBContext.ProductStatuses
                        .AsNoTracking()
                        .FirstOrDefaultAsync(ps => ps.Id == productStatusId));

                return productStatusDTO;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return null;
            }
        }

        public async Task<ProductStatusDTO> TakeProductStatusWithPredicate(Expression<Func<ProductStatus, bool>> predicate)
        {
            try
            {
                var productStatusDTO = await _pmDBContext.ProductStatuses
                    .AsNoTracking()
                    .Where(predicate)
                    .Select(ps => ProductStatusMapper.ProductStatusToProductStatusDTO(ps))
                    .FirstOrDefaultAsync();

                return productStatusDTO;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return null;
            }
        }

        public async Task<Response> UpdateProductStatus(ProductStatusDTO productStatusDTO)
        {
            try
            {
                var productStatus = await _pmDBContext.ProductStatuses.FindAsync(productStatusDTO.Id);

                if (productStatus == null)
                {
                    return new Response(false, "Product Status not Found!");
                }

                ApplyPropertiesFromDTOToModel(productStatusDTO, productStatus);

                _pmDBContext.ProductStatuses.Update(productStatus);
                await _pmDBContext.SaveChangesAsync();

                return new Response(true, "Successfully Updated!");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while updating Product Status!");
            }
        }

        private void ApplyPropertiesFromDTOToModel(ProductStatusDTO productStatusDTO, ProductStatus productStatus)
        {
            var dtoProperties = productStatusDTO.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var modelProperties = productStatus.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var dtoProperty in dtoProperties)
            {
                var modelProperty = modelProperties.FirstOrDefault(p => p.Name == dtoProperty.Name && p.PropertyType == dtoProperty.PropertyType);
                if (modelProperty != null)
                {
                    modelProperty.SetValue(productStatus, dtoProperty.GetValue(productStatusDTO));
                }
            }
        }
    }
}
