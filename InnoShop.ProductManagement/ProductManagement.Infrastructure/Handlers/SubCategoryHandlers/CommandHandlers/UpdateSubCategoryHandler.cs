using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.Commands.SubCategoryCommands;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.Infrastructure.Handlers.SubCategoryHandlers.CommandHandlers
{
    public class UpdateSubCategoryHandler : IRequestHandler<UpdateSubCategoryCommand, Response>
    {
        private readonly ISubCategory _subCategoryRepository;
        public UpdateSubCategoryHandler(ISubCategory subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
        }
        public async Task<Response> Handle(UpdateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _subCategoryRepository.UpdateSubCategory(request.SubCategoryDTO);
        }
    }
}
