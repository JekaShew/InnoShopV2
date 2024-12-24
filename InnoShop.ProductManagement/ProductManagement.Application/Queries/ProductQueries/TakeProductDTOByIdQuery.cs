using InnoShop.CommonLibrary.CommonDTOs;
using MediatR;

namespace ProductManagement.Application.Queries.ProductQueries
{
    public class TakeProductDTOByIdQuery : IRequest<ProductDTO>
    {
        public Guid Id { get; set; }
    }
}
