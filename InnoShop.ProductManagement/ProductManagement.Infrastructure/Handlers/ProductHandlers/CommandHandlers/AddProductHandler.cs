using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.Commands.ProductCommands;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.Infrastructure.Handlers.ProductHandlers.CommandHandlers
{
    public class AddProductHandler : IRequestHandler<AddProductCommand, Response>
    {
        private readonly IProduct _productRepository;
        public AddProductHandler(IProduct productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Response> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            request.ProductDTO.Id = Guid.NewGuid();
            
            return await _productRepository.AddProduct(request.ProductDTO);
        }
    }
}
