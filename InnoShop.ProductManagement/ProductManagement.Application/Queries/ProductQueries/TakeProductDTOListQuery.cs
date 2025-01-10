using InnoShop.CommonLibrary.CommonDTOs;
using MediatR;

namespace ProductManagement.Application.Queries.ProductQueries
{
    public class TakeProductDTOListQuery : IRequest<List<ProductDTO>>
    {

    }
}
