using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Mappers;
using ProductManagement.Application.Queries.ProductStatusQueries;
using ProductManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.Handlers.ProductStatusHandlers.QueryHandlers
{
    public class TakeProductStatusDTOByIdHandler : IRequestHandler<TakeProductStatusDTOByIdQuery, ProductStatusDTO>
    {
        private readonly ProductManagementDBContext _pmDBContext;
        public TakeProductStatusDTOByIdHandler(ProductManagementDBContext pmDBContext)
        {
            _pmDBContext = pmDBContext;
        }
        public async Task<ProductStatusDTO> Handle(TakeProductStatusDTOByIdQuery request, CancellationToken cancellationToken)
        {
            var productStatusDTO = ProductStatusMapper.ProductStatusToProductStatusDTO(
                    await _pmDBContext.ProductStatuses
                        .AsNoTracking()
                        .FirstOrDefaultAsync(ps => ps.Id == request.Id));

            return productStatusDTO;
        }
    }
}
