using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.Commands.ProductStatusCommands;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.Infrastructure.Handlers.ProductStatusHandlers.CommandHandlers
{
    public class AddProductStatusHandler : IRequestHandler<AddProductStatusCommand, Response>
    {
        private readonly IProductStatus _productStatusRepository;
        public AddProductStatusHandler(IProductStatus productStatusRepository)
        {
            _productStatusRepository = productStatusRepository;
        }
        public async Task<Response> Handle(AddProductStatusCommand request, CancellationToken cancellationToken)
        {
            request.ProductStatusDTO.Id = Guid.NewGuid();

            return await _productStatusRepository.AddProductStatus(request.ProductStatusDTO);
        }
    }
}
