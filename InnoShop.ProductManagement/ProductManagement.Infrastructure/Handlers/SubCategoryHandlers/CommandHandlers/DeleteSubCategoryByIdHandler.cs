using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.Commands.SubCategoryCommands;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.Infrastructure.Handlers.SubCategoryHandlers.CommandHandlers
{
    public class DeleteSubCategoryByIdHandler : IRequestHandler<DeleteSubCategoryByIdCommand, Response>
    {
        private readonly ISubCategory _subCategoryRepository;
        public DeleteSubCategoryByIdHandler(ISubCategory subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
        }
        public async Task<Response> Handle(DeleteSubCategoryByIdCommand request, CancellationToken cancellationToken)
        {
            return await _subCategoryRepository.DeleteSubCategoryById(request.Id);
        }
    }
}
