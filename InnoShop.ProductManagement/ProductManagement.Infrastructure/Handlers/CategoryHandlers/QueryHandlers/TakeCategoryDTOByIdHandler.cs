using MediatR;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Queries.CategoryQueries;

namespace ProductManagement.Infrastructure.Handlers.CategoryHandlers.QueryHandlers
{
    public class TakeCategoryDTOByIdHandler : IRequestHandler<TakeCategoryDTOByIdQuery, CategoryDTO>
    {
        private readonly ICategory _categoryRepository;
        public TakeCategoryDTOByIdHandler(ICategory categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<CategoryDTO> Handle(TakeCategoryDTOByIdQuery request, CancellationToken cancellationToken)
        {            
            return await _categoryRepository.TakeCategoryById(request.Id);
        }
    }
}
