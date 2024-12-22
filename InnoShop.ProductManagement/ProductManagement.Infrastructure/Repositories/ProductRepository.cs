using InnoShop.CommonLibrary.Logs;
using InnoShop.CommonLibrary.Response;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.Repositories
{
    public class ProductRepository : IProduct
    {
        public Task<Response> AddProduct(ProductDTO productDTO)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while adding new Product!");
            }
        }

        public Task<Response> DeleteProductById(Guid productId)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while deleting Product!");
            }
        }

        public Task<List<ProductDTO>> TakeAllProducts()
        {
            try
            {

            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while taking all Products!");
            }
        }

        public Task<ProductDTO> TakeProductById(Guid productId)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while taking Product!");
            }
        }

        public Task<Response> UpdateProduct(ProductDTO productDTO)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while updating Product!");
            }
        }
    }
}
