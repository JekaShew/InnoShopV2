using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Mappers;
using ProductManagement.Application.Queries.CategoryQueries;
using ProductManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.Handlers.CategoryHandlers.QueryHandlers
{
    public class TakeCategoryDTOListHandler : IRequestHandler<TakeCategoryDTOListQuery, List<CategoryDTO>>
    {
        private readonly ProductManagementDBContext _pmDBContext;
        public TakeCategoryDTOListHandler(ProductManagementDBContext pmDBContext)
        {
            _pmDBContext = pmDBContext;
        }
        public async Task<List<CategoryDTO>> Handle(TakeCategoryDTOListQuery request, CancellationToken cancellationToken)
        {
            var categoryDTOs = await _pmDBContext.Categories
                                .AsNoTracking()
                                .Select(c => CategoryMapper.CategoryToCategoryDTO(c))
                                .ToListAsync(cancellationToken);

            return categoryDTOs;
        }
    }
}
