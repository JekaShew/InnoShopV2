using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Mappers;
using ProductManagement.Application.Queries.SubCategoryQueries;
using ProductManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.Handlers.SubCategoryHandlers.QueryHandlers
{
    public class TakeSubCategoryDTOListHandler : IRequestHandler<TakeSubCategoryDTOListQuery, List<SubCategoryDTO>>
    {
        private readonly ProductManagementDBContext _pmDBContext;
        public TakeSubCategoryDTOListHandler(ProductManagementDBContext pmDBContext)
        {
            _pmDBContext = pmDBContext;
        }
        public async Task<List<SubCategoryDTO>> Handle(TakeSubCategoryDTOListQuery request, CancellationToken cancellationToken)
        {
            return await _pmDBContext.SubCategories
                    .Include(c => c.Category)
                    .AsNoTracking()
                    .Select(sc => SubCategoryMapper.SubCategoryToSubCategoryDTO(sc))
                    .ToListAsync(cancellationToken);
        }
    }
}
