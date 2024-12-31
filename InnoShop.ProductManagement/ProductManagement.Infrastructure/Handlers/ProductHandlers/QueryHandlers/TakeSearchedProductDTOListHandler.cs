using InnoShop.CommonLibrary.CommonDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Mappers;
using ProductManagement.Application.Queries.ProductQueries;
using ProductManagement.Infrastructure.Data;

namespace ProductManagement.Infrastructure.Handlers.ProductHandlers.QueryHandlers
{
    public class TakeSearchedProductDTOListHandler : IRequestHandler<TakeSearchedProductDTOListQuery, List<ProductDTO>>
    {
        private readonly ProductManagementDBContext _pmDBContext;
        public TakeSearchedProductDTOListHandler(ProductManagementDBContext pmDBContext)
        {
            _pmDBContext = pmDBContext;
        }
        public async Task<List<ProductDTO>> Handle(TakeSearchedProductDTOListQuery request, CancellationToken cancellationToken)
        {
            var searchedProductDTOList = await _pmDBContext.Products
                        .Where(p => p.Title
                            .ToLower()
                            .Contains(request.QueryString) 
                            || p.Description
                                .ToLower()
                                .Contains(request.QueryString))
                        .Select(p => ProductMapper.ProductToProductDTO(p))
                        .ToListAsync(cancellationToken);

            return searchedProductDTOList;
        }
    }
}
