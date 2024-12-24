using Azure;
using InnoShop.CommonLibrary.CommonDTOs;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Services
{
    public class ProductService : IProductServices
    {
        public async Task<Response> ChangeProductStatusOfProduct(Guid productId, Guid productStatusId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDTO>> TakeFilteredProductDTOList(ProductFilterDTO productFilterDTO)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDTO>> TakeProductsByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDTO>> TakeSearchedProductDTOList(string query)
        {
            throw new NotImplementedException();
        }
    }
}
