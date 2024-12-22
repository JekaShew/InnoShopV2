using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    public class TakeProductStatusDTOListHandler : IRequestHandler<TakeProductStatusDTOListQuery, List<ProductStatusDTO>>
    {
        private readonly ProductManagementDBContext _pmDBContext;
        public TakeProductStatusDTOListHandler(ProductManagementDBContext pmDBContext)
        {
            _pmDBContext = pmDBContext;
        }
        public async Task<List<ProductStatusDTO>> Handle(TakeProductStatusDTOListQuery request, CancellationToken cancellationToken)
        {
            var productStatusDTOs = await _pmDBContext.ProductStatuses
                        .AsNoTracking()
                        .Select(ps => ProductStatusMapper.ProductStatusToProductStatusDTO(ps))
                        .ToListAsync(cancellationToken);

            return productStatusDTOs;
        }
    }
}
