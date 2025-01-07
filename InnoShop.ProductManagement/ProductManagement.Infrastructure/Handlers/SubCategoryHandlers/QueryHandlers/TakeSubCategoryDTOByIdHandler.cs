using MediatR;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Queries.SubCategoryQueries;

namespace ProductManagement.Infrastructure.Handlers.SubCategoryHandlers.QueryHandlers
{
    public class TakeSubCategoryDTOByIdHandler : IRequestHandler<TakeSubCategoryDTOByIdQuery, SubCategoryDTO>
    {
        private readonly ISubCategory _subCategoryRepository;
        public TakeSubCategoryDTOByIdHandler(ISubCategory subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
        }

        public async Task<SubCategoryDTO> Handle(TakeSubCategoryDTOByIdQuery request, CancellationToken cancellationToken)
        {
            return await _subCategoryRepository.TakeSubCategoryById(request.Id);
        }
    }
}
