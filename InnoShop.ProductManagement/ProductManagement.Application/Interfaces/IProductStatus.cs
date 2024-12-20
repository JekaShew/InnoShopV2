using InnoShop.CommonLibrary.Response;
using ProductManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Interfaces
{
    public interface IProductStatus
    {
        public Task<Response> AddProductStatus(ProductStatusDTO productStatusDTO);
        public Task<List<ProductStatusDTO>> TakeAllProductStatuses();
        public Task<ProductStatusDTO> TakeProductStatusById(Guid productStatusId);
        public Task<Response> UpdateProductStatus(ProductStatusDTO productStatusDTO);
        public Task<Response> DeleteProductStatusById(Guid productStatusId);
    }
}
