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
    public class AddCategoryHandler : IRequestHandler<AddCategoryCommand, Response>
    {
        private readonly ProductManagementDBContext _pmDBContext;
        public AddCategoryHandler(ProductManagementDBContext pmDBContext)
        {
            _pmDBContext = pmDBContext;
        }
        public async Task<Response> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            await _pmDBContext.Categories.AddAsync(CategoryMapper.CategoryDTOToCategory(request.CategoryDTO));
            await _pmDBContext.SaveChangesAsync();

            return new Response(true, "Successfully Added!");
        }
    }
}
