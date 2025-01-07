using InnoShop.CommonLibrary.CommonDTOs;
using InnoShop.CommonLibrary.Response;
using ProductManagement.Domain.Data.Models;
using System.Linq.Expressions;

namespace ProductManagement.Application.Interfaces
{
    public interface IProduct
    {
        public Task<Response> AddProduct(ProductDTO productDTO);
        public Task<List<ProductDTO>> TakeAllProducts();
        public Task<ProductDTO> TakeProductById(Guid productId);
        public Task<Response> UpdateProduct(ProductDTO productDTO);
        public Task<Response> DeleteProductById(Guid productId);
        public Task<List<ProductDTO>> TakeProductsWithPredicate(Expression<Func<Product, bool>> predicate);
    }
}
