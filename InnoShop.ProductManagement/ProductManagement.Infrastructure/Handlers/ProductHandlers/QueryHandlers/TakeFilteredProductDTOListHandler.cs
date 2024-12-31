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
            decimal minValue = request.ProductFilterDTO.MinPrice != null ? (decimal)request.ProductFilterDTO.MinPrice : 0.01M;
            decimal maxValue = request.ProductFilterDTO.MaxPrice != null ? (decimal)request.ProductFilterDTO.MaxPrice : decimal.MaxValue;

            var subCategoryIds = request.ProductFilterDTO.SubCategory != null ? request.ProductFilterDTO.SubCategory.Select(sc=> sc.Id).ToList()
                : (await _pmDBContext.SubCategories.AsNoTracking().Select(sc => sc.Id).ToListAsync());
            var categoryIds = request.ProductFilterDTO.Category != null ? request.ProductFilterDTO.Category.Select(sc => sc.Id).ToList()
                : (await _pmDBContext.Categories.AsNoTracking().Select(sc => sc.Id).ToListAsync());

            var filteredProductDTOList = await _pmDBContext.Products
                    .AsNoTracking()    
                    .Where(p => 
                    decimal.Compare(p.Price, minValue) > 0
                    && 
                    decimal.Compare(p.Price, maxValue) < 0
                    && 
                    subCategoryIds.Contains(p.SubCategoryId)
                    && 
                    categoryIds.Contains(p.SubCategory.CategoryId))
                    .Select(p => ProductMapper.ProductToProductDTO(p))
                    .ToListAsync(cancellationToken);

            var products = _pmDBContext.Products.Select(fp => new { Title = fp.Title, Price = fp.Price }).ToList();
            var filteredPrices = filteredProductDTOList.Select(fp => new { Title = fp.Title, Price = fp.Price }).ToList();
            return filteredProductDTOList;
        }
    }
}
