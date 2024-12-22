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
    public class TakeCategoryDTOByIdHandler : IRequestHandler<TakeCategoryDTOByIdQuery, CategoryDTO>
    {
        private readonly ProductManagementDBContext _pmDBContext;
        public TakeCategoryDTOByIdHandler(ProductManagementDBContext pmDBContext)
        {
            _pmDBContext = pmDBContext;
        }
        public async Task<CategoryDTO> Handle(TakeCategoryDTOByIdQuery request, CancellationToken cancellationToken)
        {
            var categoryDTO = CategoryMapper.CategoryToCategoryDTO(
                await _pmDBContext.Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == request.Id));
            
            return categoryDTO;
        }
    }
}
