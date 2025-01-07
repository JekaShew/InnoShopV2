using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.Commands.ProductCommands;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.Infrastructure.Handlers.ProductHandlers.CommandHandlers
{
    public class DeleteProductByIdHandler : IRequestHandler<DeleteProductByIdCommand, Response>
    {
        private readonly IProduct _productRepository;
        public DeleteProductByIdHandler(IProduct productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Response> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {
            return await _productRepository.DeleteProductById(request.Id);
        }
    }
}
