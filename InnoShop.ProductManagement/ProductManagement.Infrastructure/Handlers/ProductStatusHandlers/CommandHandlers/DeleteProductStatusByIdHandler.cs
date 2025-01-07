using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.Commands.ProductStatusCommands;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.Infrastructure.Handlers.ProductStatusHandlers.CommandHandlers
{
    public class DeleteProductStatusByIdHandler : IRequestHandler<DeleteProductStatusByIdCommand, Response>
    {
        private readonly IProductStatus _productStatusRepository;
        public DeleteProductStatusByIdHandler(IProductStatus productStatusRepository)
        {
            _productStatusRepository = productStatusRepository;
        }
        public async Task<Response> Handle(DeleteProductStatusByIdCommand request, CancellationToken cancellationToken)
        {
            return await _productStatusRepository.DeleteProductStatusById(request.Id);
        }
    }
}
