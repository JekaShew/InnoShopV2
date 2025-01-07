using InnoShop.CommonLibrary.CommonDTOs;
using MediatR;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Queries.ProductQueries;

namespace ProductManagement.Infrastructure.Handlers.ProductHandlers.QueryHandlers
{
    public class TakeProductDTOByIdHandler : IRequestHandler<TakeProductDTOByIdQuery, ProductDTO>
    {
        private readonly IProduct _productRepository;
        public TakeProductDTOByIdHandler(IProduct productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ProductDTO> Handle(TakeProductDTOByIdQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.TakeProductById(request.Id);
        }
    }
}
