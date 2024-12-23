using InnoShop.CommonLibrary.Response;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    public class AddProductStatusHandler : IRequestHandler<AddProductStatusCommand, Response>
    {
        private readonly ProductManagementDBContext _pmDBContext;
        public AddProductStatusHandler(ProductManagementDBContext pmDBContext)
        {
            _pmDBContext = pmDBContext;
        }
        public async Task<Response> Handle(AddProductStatusCommand request, CancellationToken cancellationToken)
        {
            request.ProductStatusDTO.Id = Guid.NewGuid();
            await _pmDBContext.ProductStatuses.AddAsync(ProductStatusMapper.ProductStatusDTOToProductStatus(request.ProductStatusDTO));
            await _pmDBContext.SaveChangesAsync();

            return new Response(true, "Successfully Added!");
        }
    }
}
