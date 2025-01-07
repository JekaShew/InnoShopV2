using InnoShop.CommonLibrary.CommonDTOs;
using MediatR;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Queries.ProductQueries;

namespace ProductManagement.Infrastructure.Handlers.ProductHandlers.QueryHandlers
{
    public class TakeFilteredProductDTOListHandler : IRequestHandler<TakeFilteredProductDTOListQuery, List<ProductDTO>>
    {
        private readonly IProduct _productRepository;
        private readonly ISubCategory _subCategoryRepository;
        private readonly ICategory _categoryRepository;
        public TakeFilteredProductDTOListHandler(
                    IProduct productRepository, 
                    ISubCategory subCategoryRepository, 
                    ICategory categoryRepository)
        {
            _productRepository = productRepository;
            _subCategoryRepository = subCategoryRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<List<ProductDTO>> Handle(TakeFilteredProductDTOListQuery request, CancellationToken cancellationToken)
        {
            decimal minValue = request.ProductFilterDTO.MinPrice != null ? (decimal)request.ProductFilterDTO.MinPrice : 0.01M;
            decimal maxValue = request.ProductFilterDTO.MaxPrice != null ? (decimal)request.ProductFilterDTO.MaxPrice : decimal.MaxValue;

            var subCategoryIds = request.ProductFilterDTO.SubCategory != null ? request.ProductFilterDTO.SubCategory.Select(sc => sc.Id).ToList()
                : (await _subCategoryRepository.TakeAllSubCategories()).Select(sc => sc.Id.Value).ToList();

            var categoryIds = request.ProductFilterDTO.Category != null ? request.ProductFilterDTO.Category.Select(sc => sc.Id).ToList()
                : (await _categoryRepository.TakeAllCategories()).Select(c => c.Id.Value).ToList();

            var filteredProductDTOList = await _productRepository.TakeProductsWithPredicate(
                    p =>
                    decimal.Compare(p.Price, minValue) > 0
                    &&
                    decimal.Compare(p.Price, maxValue) < 0
                    &&
                    subCategoryIds.Contains(p.SubCategoryId)
                    &&
                    categoryIds.Contains(p.SubCategory.CategoryId)
                );
               
            return filteredProductDTOList;
        }
    }
}
