using InnoShop.CommonLibrary.CommonDTOs;
using MediatR;

namespace ProductManagement.Application.Queries.ProductQueries
{
    public class TakeSearchedProductDTOListQuery : IRequest<List<ProductDTO>>
    {
        public string? QueryString { get; set; }
    }
}
