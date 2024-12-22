using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.Commands.ProductStatusCommands;
using ProductManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.Handlers.ProductStatusHandlers.CommandHandlers
{
    public class DeleteProductStatusByIdHandler : IRequestHandler<DeleteProductStatusByIdCommand, Response>
    {
        private readonly ProductManagementDBContext _pmDBContext;
        public DeleteProductStatusByIdHandler(ProductManagementDBContext pmDBContext)
        {
            _pmDBContext = pmDBContext;
        }
        public async Task<Response> Handle(DeleteProductStatusByIdCommand request, CancellationToken cancellationToken)
        {
            var productStatus = await _pmDBContext.ProductStatuses.FindAsync(request.Id);
            if (productStatus == null)
                return new Response(false, "Product Status not found!");
            _pmDBContext.ProductStatuses.Remove(productStatus);
            await _pmDBContext.SaveChangesAsync();
            return new Response(true, "Successfully Deleted!");
        }
    }
}
