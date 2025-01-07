using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.Commands.CategoryCommands;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.Infrastructure.Handlers.CategoryHandlers.CommandHandlers
{
    public class AddCategoryHandler : IRequestHandler<AddCategoryCommand, Response>
    {
        private readonly ICategory _categoryRepository;
        public AddCategoryHandler(ICategory categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Response> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            request.CategoryDTO.Id = Guid.NewGuid();
            
            return await _categoryRepository.AddCategory(request.CategoryDTO);
        }
    }
}
