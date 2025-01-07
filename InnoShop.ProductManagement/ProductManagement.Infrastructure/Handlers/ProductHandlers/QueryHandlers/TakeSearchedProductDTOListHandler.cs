using InnoShop.CommonLibrary.CommonDTOs;
using MediatR;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Queries.ProductQueries;

namespace ProductManagement.Infrastructure.Handlers.ProductHandlers.QueryHandlers
{
    public class TakeSearchedProductDTOListHandler : IRequestHandler<TakeSearchedProductDTOListQuery, List<ProductDTO>>
    {
        private readonly IProduct _productRepository;
        public TakeSearchedProductDTOListHandler(IProduct productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<ProductDTO>> Handle(TakeSearchedProductDTOListQuery request, CancellationToken cancellationToken)
        {
            var searchedProductDTOList = await _productRepository
                .TakeProductsWithPredicate(p => p.Title
                            .ToLower()
                            .Contains(request.QueryString)
                            || p.Description
                                .ToLower()
                                .Contains(request.QueryString));

            return searchedProductDTOList;
        }
    }
}
