using InnoShop.CommonLibrary.CommonDTOs;
using InnoShop.CommonLibrary.Response;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Interfaces
{
    public interface IProductServices
    {
        public Task<List<ProductDTO>> TakeProductsByUserId(Guid userId);
        public Task<List<ProductDTO>> TakeFilteredProductDTOList(ProductFilterDTO productFilterDTO);
        public Task<List<ProductDTO>> TakeSearchedProductDTOList(string query);
        public Task<Response> ChangeProductStatusOfProduct(Guid productId, Guid productStatusId);
        public Task<Response> ChangeProductStatusesOfProductsByUserId(Guid userId);
        public Task<Response> CreateProduct(ProductDTO productDTO);
        public Guid? GetCurrentUserId();
    }
}
