using MediatR;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Queries.CategoryQueries;

namespace ProductManagement.Infrastructure.Handlers.CategoryHandlers.QueryHandlers
{
    public class TakeCategoryDTOListHandler : IRequestHandler<TakeCategoryDTOListQuery, List<CategoryDTO>>
    {
        private readonly ICategory _categoryRepository;
        public TakeCategoryDTOListHandler(ICategory categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<List<CategoryDTO>> Handle(TakeCategoryDTOListQuery request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.TakeAllCategories();
        }
    }
}
