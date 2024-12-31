using InnoShop.CommonLibrary.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProductManagement.Application.Commands.SubCategoryCommands;
using ProductManagement.Application.Mappers;
using ProductManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.Handlers.SubCategoryHandlers.CommandHandlers
{
    public class AddSubCategoryHandler : IRequestHandler<AddSubCategoryCommand, Response>
    {
        private readonly ProductManagementDBContext _pmDBContext;
        public AddSubCategoryHandler(ProductManagementDBContext pmDBContext)
        {
            _pmDBContext = pmDBContext;
        }
        public async Task<Response> Handle(AddSubCategoryCommand request, CancellationToken cancellationToken)
        {
            request.SubCategoryDTO.Id = Guid.NewGuid();
            if (!await _pmDBContext.Categories.AnyAsync(c => c.Id == request.SubCategoryDTO.CategoryId))
                return new Response(false, "No Categories Id matches Your SubCategory!");
            await _pmDBContext.SubCategories.AddAsync(SubCategoryMapper.SubCategoryDTOToSubCategory(request.SubCategoryDTO));
            await _pmDBContext.SaveChangesAsync();
         
            return new Response(true, "Successfully Added!");
        }
    }
}
