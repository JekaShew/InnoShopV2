using InnoShop.CommonLibrary.Response;
using ProductManagement.Application.DTOs;
using ProductManagement.Domain.Data.Models;
using System.Linq.Expressions;

namespace ProductManagement.Application.Interfaces
{
    public interface IProductStatus
    {
        public Task<Response> AddProductStatus(ProductStatusDTO productStatusDTO);
        public Task<List<ProductStatusDTO>> TakeAllProductStatuses();
        public Task<ProductStatusDTO> TakeProductStatusById(Guid productStatusId);
        public Task<Response> UpdateProductStatus(ProductStatusDTO productStatusDTO);
        public Task<Response> DeleteProductStatusById(Guid productStatusId);
        public Task<ProductStatusDTO> TakeProductStatusWithPredicate(Expression<Func<ProductStatus, bool>> predicate);
    }
}
