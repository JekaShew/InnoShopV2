using Azure;
using InnoShop.CommonLibrary.CommonDTOs;
using ProductManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Interfaces
{
    public interface IProductServices
    {
        public Task<List<ProductDTO>> TakeProductsByUserId(Guid userId);
        public Task<List<ProductDTO>> TakeFilteredProductDTOList(ProductFilterDTO productFilterDTO);
        public Task<List<ProductDTO>> TakeSearchedProductDTOList(string query);
        public Task<Response> ChangeProductStatusOfProduct(Guid productId, Guid productStatusId);

    }
}
