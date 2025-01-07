using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.Commands.CategoryCommands;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.Infrastructure.Handlers.CategoryHandlers.CommandHandlers
{

    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Response>
    {
        private readonly ICategory _categoryRepository;
        public UpdateCategoryHandler(ICategory categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Response> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.UpdateCategory(request.CategoryDTO);
        }
    }
}
