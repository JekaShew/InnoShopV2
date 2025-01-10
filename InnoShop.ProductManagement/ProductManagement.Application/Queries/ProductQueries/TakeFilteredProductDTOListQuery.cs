using InnoShop.CommonLibrary.CommonDTOs;
using MediatR;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Queries.ProductQueries
{
    public class TakeFilteredProductDTOListQuery : IRequest<List<ProductDTO>>
    {
        public ProductFilterDTO ProductFilterDTO { get; set; }
    }
}
