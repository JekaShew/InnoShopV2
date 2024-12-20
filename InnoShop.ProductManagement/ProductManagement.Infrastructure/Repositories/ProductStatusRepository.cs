using InnoShop.CommonLibrary.Logs;
using InnoShop.CommonLibrary.Response;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.Repositories
{
    public class ProductStatusRepository(ProductManagementDBContext dBContext) : IProductStatus
    {
        public async Task<Response> AddProductStatus(ProductStatusDTO productStatusDTO)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false,"Error while adding new Product Status!");
            }
        }

        public Task<Response> DeleteProductStatusById(Guid productStatusId)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while deleting Product Status!");
            }
        }

        public Task<List<ProductStatusDTO>> TakeAllProductStatuses()
        {
            try
            {

            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while taking all Product Statuses!");
            }
        }

        public Task<ProductStatusDTO> TakeProductStatusById(Guid productStatusId)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while taking Product Status!");
            }
        }

        public Task<Response> UpdateProductStatus(ProductStatusDTO productStatusDTO)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while updating Product Status!");
            }
        }
    }
}
