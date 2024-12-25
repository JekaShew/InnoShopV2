using InnoShop.CommonLibrary.CommonDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class TakeProductDTOListByUserIdHandler : IRequestHandler<TakeProductDTOListByUserIdQuery, List<ProductDTO>>
    {
        private readonly ProductManagementDBContext _pmDBContext;
        public TakeProductDTOListByUserIdHandler(ProductManagementDBContext pmDBComtext)
        {
            _pmDBContext = pmDBComtext;
        }
        public async Task<List<ProductDTO>> Handle(TakeProductDTOListByUserIdQuery request, CancellationToken cancellationToken)
        {
           var userProductDTOs = await _pmDBContext.Products.Where(p => p.UserId == request.UserId).Select(p => ProductMapper.ProductToProductDTO(p)).ToListAsync();

            return userProductDTOs;
        }
    }
}
