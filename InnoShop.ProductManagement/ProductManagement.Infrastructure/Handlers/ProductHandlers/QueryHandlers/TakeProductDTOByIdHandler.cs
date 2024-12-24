using InnoShop.CommonLibrary.CommonDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Mappers;
using ProductManagement.Application.Queries.ProductQueries;
using ProductManagement.Infrastructure.Data;

namespace ProductManagement.Infrastructure.Handlers.ProductHandlers.QueryHandlers
{
    public class TakeProductDTOByIdHandler : IRequestHandler<TakeProductDTOByIdQuery, ProductDTO>
    {
        private readonly ProductManagementDBContext _pmDBContext;
        public TakeProductDTOByIdHandler(ProductManagementDBContext pmDBContext)
        {
            _pmDBContext = pmDBContext;
        }
        public async Task<ProductDTO> Handle(TakeProductDTOByIdQuery request, CancellationToken cancellationToken)
        {
            var productDTO = ProductMapper.ProductToProductDTO(await _pmDBContext.Products
                    .Include(ps => ps.ProductStatus)
                    .Include(sc => sc.SubCategory)
                        .ThenInclude(sc => sc.Category)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == request.Id));

            return productDTO;
        }
    }
}
