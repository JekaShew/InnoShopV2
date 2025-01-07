using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.Commands.ProductCommands;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.Infrastructure.Handlers.ProductHandlers.CommandHandlers
{
    public class ChangeProductStatusOfProductHandler : IRequestHandler<ChangeProductStatusOfProductCommand, Response>
    {
        private readonly IProduct _productRepository;
        private readonly IProductStatus _productStatusRepository;
        public ChangeProductStatusOfProductHandler(
                    IProduct productRepository, 
                    IProductStatus productStatusRepository)
        {
            _productRepository = productRepository;
            _productStatusRepository = productStatusRepository;
        }
        public async Task<Response> Handle(ChangeProductStatusOfProductCommand request, CancellationToken cancellationToken)
        {
            if(await _productStatusRepository.TakeProductStatusById(request.ProductStatusId) is null)
                return new Response(false, "Product Status not found");

            var productDTO = await _productRepository.TakeProductById(request.ProductId);

            if (productDTO is null)
                return new Response(false, "Product not found!");

            productDTO.ProductStatusId = request.ProductStatusId;

            return await _productRepository.UpdateProduct(productDTO);
        }
    }
}
