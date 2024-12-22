using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.Commands.CategoryCommands;
using ProductManagement.Application.Mappers;
using ProductManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.Handlers.CategoryHandlers.CommandHandlers
{

    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Response>
    {
        private readonly ProductManagementDBContext _pmDBContext;
        public UpdateCategoryHandler(ProductManagementDBContext pmDBContext)
        {
            _pmDBContext = pmDBContext;
        }

        public async Task<Response> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            _pmDBContext.Categories.Update(CategoryMapper.CategoryDTOToCategory(request.CategoryDTO));
            await _pmDBContext.SaveChangesAsync();

            return new Response(true, "Successfully Updated!");
        }
    }
}
