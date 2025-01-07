using InnoShop.CommonLibrary.CommonDTOs;
using MediatR;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Queries.ProductQueries;

namespace ProductManagement.Infrastructure.Handlers.ProductHandlers.QueryHandlers
{
    public class TakeProductDTOListHandler : IRequestHandler<TakeProductDTOListQuery, List<ProductDTO>>
    {
        private readonly IProduct _productRepository;
        public TakeProductDTOListHandler(IProduct productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<ProductDTO>> Handle(TakeProductDTOListQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.TakeAllProducts();
        }
    }
}
