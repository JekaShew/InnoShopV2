using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.Commands.ProductStatusCommands;
using ProductManagement.Application.Mappers;
using ProductManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.Handlers.ProductStatusHandlers.CommandHandlers
{
    public class UpdateProductStatusHandler : IRequestHandler<UpdateProductStatusCommand, Response>
    {
        private readonly ProductManagementDBContext _pmDBContext;
        public UpdateProductStatusHandler(ProductManagementDBContext pmDBContext)
        {
            _pmDBContext = pmDBContext;
        }
        public async Task<Response> Handle(UpdateProductStatusCommand request, CancellationToken cancellationToken)
        {
            _pmDBContext.ProductStatuses.Update(ProductStatusMapper.ProductStatusDTOToProductStatus(request.ProductStatusDTO));
            await _pmDBContext.SaveChangesAsync();

            return new Response(true, "Successfully Updated!");
        }
    }
}
