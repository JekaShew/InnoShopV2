using InnoShop.CommonLibrary.Response;
using ProductManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Interfaces
{
    public interface IProduct
    {
        public Task<Response> AddProduct(ProductDTO productDTO);
        public Task<List<ProductDTO>> TakeAllProducts();
        public Task<ProductDTO> TakeProductById(Guid productId);
        public Task<Response> UpdateProduct(ProductDTO productDTO);
        public Task<Response> DeleteProductById(Guid productId);
    }
}
