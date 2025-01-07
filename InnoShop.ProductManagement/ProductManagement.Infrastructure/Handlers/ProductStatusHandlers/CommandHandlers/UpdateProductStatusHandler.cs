using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.Commands.ProductStatusCommands;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.Infrastructure.Handlers.ProductStatusHandlers.CommandHandlers
{
    public class UpdateProductStatusHandler : IRequestHandler<UpdateProductStatusCommand, Response>
    {
        private readonly IProductStatus _productStatusRepository;
        public UpdateProductStatusHandler(IProductStatus productStatusRepository)
        {
            _productStatusRepository = productStatusRepository;
        }
        public async Task<Response> Handle(UpdateProductStatusCommand request, CancellationToken cancellationToken)
        {
            return await _productStatusRepository.UpdateProductStatus(request.ProductStatusDTO);
        }
    }
}
