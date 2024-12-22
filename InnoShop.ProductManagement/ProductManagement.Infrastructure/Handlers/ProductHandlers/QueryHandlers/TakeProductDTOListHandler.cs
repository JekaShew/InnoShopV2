using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Mappers;
using ProductManagement.Application.Queries.ProductQueries;
using ProductManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.Handlers.ProductHandlers.QueryHandlers
{
    public class TakeProductDTOListHandler : IRequestHandler<TakeProductDTOListQuery, List<ProductDTO>>
    {
        private readonly ProductManagementDBContext _pmDBContext;
        public TakeProductDTOListHandler(ProductManagementDBContext pmDBContext)
        {
            _pmDBContext = pmDBContext;
        }
        public async Task<List<ProductDTO>> Handle(TakeProductDTOListQuery request, CancellationToken cancellationToken)
        {
            var productDTOs = await _pmDBContext.Products
                    .Include(ps=> ps.ProductStatus)
                    .Include(sc => sc.SubCategory)
                        .ThenInclude(sc=> sc.Category)
                    .AsNoTracking()
                    .Select(p => ProductMapper.ProductToProductDTO(p))
                    .ToListAsync(cancellationToken);

            return productDTOs;
        }
    }
}
