using MediatR;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Queries.ProductStatusQueries
{
    public class TakeProductStatusDTOListQuery : IRequest<List<ProductStatusDTO>>
    {

    }
}
