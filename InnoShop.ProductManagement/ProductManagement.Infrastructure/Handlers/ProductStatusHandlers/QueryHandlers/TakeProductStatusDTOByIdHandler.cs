using MediatR;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Queries.ProductStatusQueries;

namespace ProductManagement.Infrastructure.Handlers.ProductStatusHandlers.QueryHandlers
{
    public class TakeProductStatusDTOByIdHandler : IRequestHandler<TakeProductStatusDTOByIdQuery, ProductStatusDTO>
    {
        private readonly IProductStatus _productStatusRepository;
        public TakeProductStatusDTOByIdHandler(IProductStatus productStatusRepository)
        {
            _productStatusRepository = productStatusRepository;
        }
        public async Task<ProductStatusDTO> Handle(TakeProductStatusDTOByIdQuery request, CancellationToken cancellationToken)
        {
            return await _productStatusRepository.TakeProductStatusById(request.Id);
        }
    }
}
