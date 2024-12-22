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
    public class TakeSubCategoryDTOByIdHandler : IRequestHandler<TakeSubCategoryDTOByIdQuery, SubCategoryDTO>
    {
        private readonly ProductManagementDBContext _pmDBContext;
        public TakeSubCategoryDTOByIdHandler(ProductManagementDBContext pmDBContext)
        {
            _pmDBContext = pmDBContext;
        }

        public async Task<SubCategoryDTO> Handle(TakeSubCategoryDTOByIdQuery request, CancellationToken cancellationToken)
        {
            return SubCategoryMapper.SubCategoryToSubCategoryDTO(
                                await _pmDBContext.SubCategories
                                            .Include(c => c.Category)
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(sc =>
                                            sc.Id == request.Id));
        }
    }
}
