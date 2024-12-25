using InnoShop.CommonLibrary.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Commands.ProductCommands;
using ProductManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.Handlers.ProductHandlers.CommandHandlers
{
    public class ChangeProductStatusOfProductHandler : IRequestHandler<ChangeProductStatusOfProductCommand, Response>
    {
        private readonly ProductManagementDBContext _pmDBContext;
        public ChangeProductStatusOfProductHandler(ProductManagementDBContext pmDBContext)
        {
            _pmDBContext = pmDBContext;
        }
        public async Task<Response> Handle(ChangeProductStatusOfProductCommand request, CancellationToken cancellationToken)
        {
            if (await _pmDBContext.ProductStatuses.FirstOrDefaultAsync(ps => ps.Id == request.ProductStatusId) is null)
                return new Response(false, "User Statuse not found");

            var product = await _pmDBContext.Products.FirstOrDefaultAsync(p => p.Id == request.ProductId);

            if (product is null)
                return new Response(false, "Product not found");

            product.ProductStatusId = request.ProductStatusId;

            await _pmDBContext.SaveChangesAsync();

            return new Response(true, "Product's Status successfully updated!");
        }
    }
}
