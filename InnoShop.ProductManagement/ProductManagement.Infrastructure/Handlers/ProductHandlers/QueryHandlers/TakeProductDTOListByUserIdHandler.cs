using InnoShop.CommonLibrary.CommonDTOs;
using MediatR;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Queries.ProductQueries;

namespace ProductManagement.Infrastructure.Handlers.ProductHandlers.QueryHandlers
{
    public class TakeProductDTOListByUserIdHandler : IRequestHandler<TakeProductDTOListByUserIdQuery, List<ProductDTO>>
    {
        private readonly IProduct _productRepository;
        public TakeProductDTOListByUserIdHandler(IProduct productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<ProductDTO>> Handle(TakeProductDTOListByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userProductDTOs =
                 await _productRepository.TakeProductsWithPredicate(p => p.UserId == request.UserId);

            return userProductDTOs;
        }
    }
}
