using InnoShop.CommonLibrary.Response;
using MediatR;
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
    public class UpdateSubCategoryHandler : IRequestHandler<UpdateSubCategoryCommand, Response>
    {
        private readonly ProductManagementDBContext _pmDBContext;
        public UpdateSubCategoryHandler(ProductManagementDBContext pmDBContext)
        {
            _pmDBContext = pmDBContext;
        }
        public async Task<Response> Handle(UpdateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            _pmDBContext.SubCategories.Update(SubCategoryMapper.SubCategoryDTOToSubCategory(request.SubCategoryDTO));
            await _pmDBContext.SaveChangesAsync();

            return new Response(true,"Successfully Updated!");
        }
    }
}
