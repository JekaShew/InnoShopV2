using InnoShop.CommonLibrary.CommonDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Mappers;
using ProductManagement.Application.Queries.ProductQueries;
using ProductManagement.Infrastructure.Data;

namespace ProductManagement.Infrastructure.Handlers.ProductHandlers.QueryHandlers
{
    public class TakeFilteredProductDTOListHandler : IRequestHandler<TakeFilteredProductDTOListQuery, List<ProductDTO>>
    {
        private readonly ProductManagementDBContext _pmDBContext;
        public TakeFilteredProductDTOListHandler(ProductManagementDBContext pmDDBContext)
        {
            _pmDBContext = pmDDBContext;
        }
        public async Task<List<ProductDTO>> Handle(TakeFilteredProductDTOListQuery request, CancellationToken cancellationToken)
        {
            var filteredProductDTOList = await _pmDBContext.Products
                    .Where(p => p.Price >= (request.ProductFilterDTO.MinPrice != null ? request.ProductFilterDTO.MinPrice : 0.01M)
                        || p.Price <= (request.ProductFilterDTO.MaxPrice != null ? request.ProductFilterDTO.MaxPrice : decimal.MaxValue)
                        || request.ProductFilterDTO.SubCategory.Any(sc => sc.Id == p.SubCategoryId)
                        || request.ProductFilterDTO.Category.Any(c => c.Id == p.SubCategory.CategoryId))
                    .Select(p => ProductMapper.ProductToProductDTO(p))
                    .ToListAsync(cancellationToken);

            return filteredProductDTOList;
        }
    }
}
