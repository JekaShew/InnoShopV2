using MediatR;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Queries.SubCategoryQueries;

namespace ProductManagement.Infrastructure.Handlers.SubCategoryHandlers.QueryHandlers
{
    public class TakeSubCategoryDTOListHandler : IRequestHandler<TakeSubCategoryDTOListQuery, List<SubCategoryDTO>>
    {
        private readonly ISubCategory _subCategoryRepository;
        public TakeSubCategoryDTOListHandler(ISubCategory subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
        }
        public async Task<List<SubCategoryDTO>> Handle(TakeSubCategoryDTOListQuery request, CancellationToken cancellationToken)
        {
            return await _subCategoryRepository.TakeAllSubCategories();
        }
    }
}
