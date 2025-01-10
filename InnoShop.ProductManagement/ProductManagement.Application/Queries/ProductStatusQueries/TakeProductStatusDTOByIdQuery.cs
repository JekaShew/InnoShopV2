using MediatR;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Queries.ProductStatusQueries
{
    public class TakeProductStatusDTOByIdQuery : IRequest<ProductStatusDTO>
    {
        public Guid Id { get; set; }
    }
}
