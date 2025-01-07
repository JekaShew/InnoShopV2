using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.Commands.CategoryCommands;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.Infrastructure.Handlers.CategoryHandlers.CommandHandlers
{
    public class DeleteCategoryByIdHandler : IRequestHandler<DeleteCategoryByIdCommand, Response>
    {
        private readonly ICategory _categoryRepository;
        
        public DeleteCategoryByIdHandler(ICategory categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Response> Handle(DeleteCategoryByIdCommand request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.DeleteCategoryById(request.Id);
        }
    }
}
