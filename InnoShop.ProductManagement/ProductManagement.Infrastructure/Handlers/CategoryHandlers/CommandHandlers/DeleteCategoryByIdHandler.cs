using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.Commands.CategoryCommands;
using ProductManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.Handlers.CategoryHandlers.CommandHandlers
{
    public class DeleteCategoryByIdHandler : IRequestHandler<DeleteCategoryByIdCommand, Response>
    {
        private readonly ProductManagementDBContext _pmDBContext;
        public DeleteCategoryByIdHandler(ProductManagementDBContext pmDBContext)
        {
            _pmDBContext = pmDBContext;
        }
        public async Task<Response> Handle(DeleteCategoryByIdCommand request, CancellationToken cancellationToken)
        {
            var category = await _pmDBContext.Categories.FindAsync(request.Id);
            if (category == null)
                return new Response(false, "Category not found!");
            _pmDBContext.Categories.Remove(category);
            await _pmDBContext.SaveChangesAsync();
            return new Response(true, "Successfully Deleted!");
        }
    }
}
