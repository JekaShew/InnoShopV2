using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.Commands.ProductCommands;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.Infrastructure.Handlers.ProductHandlers.CommandHandlers
{
    public class ChangeProductStatusesOfProductsByUserIdHandler : IRequestHandler<ChangeProductStatusesOfProductsByUserIdCommand, Response>
    {
        private readonly IProduct _productRepository;
        private readonly IProductStatus _productStatusRepository;
        public ChangeProductStatusesOfProductsByUserIdHandler(
                IProduct productRepository, 
                IProductStatus productStatusRepository)
        {
            _productRepository = productRepository;
            _productStatusRepository = productStatusRepository;
        }
        public async Task<Response> Handle(ChangeProductStatusesOfProductsByUserIdCommand request, CancellationToken cancellationToken)
        {
            var disabledProductStatusId = (await _productStatusRepository
                    .TakeProductStatusWithPredicate(ps => ps.Title == "Disabled")).Id;

            if (disabledProductStatusId == null)
                return new Response(false, "There is No Default Product Status \"Disabled\" detected!");

            var productDTOs = await _productRepository.TakeProductsWithPredicate(p => p.UserId == request.UserId);
            productDTOs.Select(p => p.ProductStatusId = disabledProductStatusId.Value);

            productDTOs.ForEach(async p => await _productRepository.UpdateProduct(p));

            return new Response(true, "Product's Statuses of User successfully changed to \"Disabled\"!");
        }
    }
}
