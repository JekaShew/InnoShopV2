using InnoShop.CommonLibrary.CommonDTOs;
using InnoShop.CommonLibrary.Response;

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
