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
    public class ChangeProductStatusesOfProductsByUserIdHandler : IRequestHandler<ChangeProductStatusesOfProductsByUserIdCommand, Response>
    {
        private readonly ProductManagementDBContext _pmDBContext;
        public ChangeProductStatusesOfProductsByUserIdHandler(ProductManagementDBContext pmDDBContext)
        {
            _pmDBContext = pmDDBContext;
        }
        public async Task<Response> Handle(ChangeProductStatusesOfProductsByUserIdCommand request, CancellationToken cancellationToken)
        {
            var disabledProductStatusId = await _pmDBContext.ProductStatuses
                                                .AsNoTracking()
                                                .Where(us => us.Title == "Disabled")
                                                .Select(us => us.Id)
                                                .FirstOrDefaultAsync();
            if (disabledProductStatusId == null)
                return new Response(false, "There is No Default Product Status \"Disabled\" detected!");

            var products = await _pmDBContext.Products
                                    .Where(p => p.UserId == request.UserId)
                                    .ToListAsync();
            products.Select(p => p.ProductStatusId = disabledProductStatusId);

            await _pmDBContext.SaveChangesAsync(cancellationToken);

            return new Response(true, "Product's Statuses successfully changed!");
        }
    }
}
