using MediatR;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Queries.ProductStatusQueries;

namespace ProductManagement.Infrastructure.Handlers.ProductStatusHandlers.QueryHandlers
{
    public class TakeProductStatusDTOListHandler : IRequestHandler<TakeProductStatusDTOListQuery, List<ProductStatusDTO>>
    {
        private readonly IProductStatus _productStatusRepository;
        public TakeProductStatusDTOListHandler(IProductStatus productStatusRepository)
        {
            _productStatusRepository = productStatusRepository;
        }
        public async Task<List<ProductStatusDTO>> Handle(TakeProductStatusDTOListQuery request, CancellationToken cancellationToken)
        {
            return await _productStatusRepository.TakeAllProductStatuses();
        }
    }
}
