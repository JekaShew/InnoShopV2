using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.Commands.ProductCommands;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.Infrastructure.Handlers.ProductHandlers.CommandHandlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Response>
    {
        private readonly IProduct _productRepository;
        public UpdateProductHandler(IProduct productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Response> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            return await _productRepository.UpdateProduct(request.ProductDTO);
        }
    }
}
