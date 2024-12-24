using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.Commands.ProductCommands;
using ProductManagement.Infrastructure.Data;

namespace ProductManagement.Infrastructure.Handlers.ProductHandlers.CommandHandlers
{
    public class DeleteProductByIdHandler : IRequestHandler<DeleteProductByIdCommand, Response>
    {
        private readonly ProductManagementDBContext _pmDBContext;
        public DeleteProductByIdHandler(ProductManagementDBContext pmDBContext)
        {
            _pmDBContext = pmDBContext;
        }

        public async Task<Response> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {
            var product = await _pmDBContext.Products.FindAsync(request.Id);
            if (product == null)
                return new Response(false, "Product not found!");
            _pmDBContext.Products.Remove(product);
            await _pmDBContext.SaveChangesAsync();

            return new Response(true, "Successfully Deleted!");
        }
    }
}
