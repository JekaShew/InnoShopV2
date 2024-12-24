using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.Commands.ProductCommands;
using ProductManagement.Application.Mappers;
using ProductManagement.Infrastructure.Data;

namespace ProductManagement.Infrastructure.Handlers.ProductHandlers.CommandHandlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Response>
    {
        private readonly ProductManagementDBContext _pmDBContext;
        public UpdateProductHandler(ProductManagementDBContext pmDBContext)
        {
            _pmDBContext = pmDBContext;
        }

        public async Task<Response> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            _pmDBContext.Products.Update(ProductMapper.ProductDTOToProduct(request.ProductDTO));
            await _pmDBContext.SaveChangesAsync();

            return new Response(true, "Successfully Updated!");
        }
    }
}
