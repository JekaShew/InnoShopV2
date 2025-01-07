using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.Commands.SubCategoryCommands;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.Infrastructure.Handlers.SubCategoryHandlers.CommandHandlers
{
    public class AddSubCategoryHandler : IRequestHandler<AddSubCategoryCommand, Response>
    {
        private readonly ISubCategory _subCategoryRepository;
        public AddSubCategoryHandler(ISubCategory subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
        }
        public async Task<Response> Handle(AddSubCategoryCommand request, CancellationToken cancellationToken)
        {
            request.SubCategoryDTO.Id = Guid.NewGuid();
           
            return await _subCategoryRepository.AddSubCategory(request.SubCategoryDTO);
        }
    }
}
