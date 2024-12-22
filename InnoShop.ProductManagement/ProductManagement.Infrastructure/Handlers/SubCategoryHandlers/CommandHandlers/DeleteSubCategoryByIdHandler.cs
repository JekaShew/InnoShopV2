using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.Commands.SubCategoryCommands;
using ProductManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.Handlers.SubCategoryHandlers.CommandHandlers
{
    public class DeleteSubCategoryByIdHandler : IRequestHandler<DeleteSubCategoryByIdCommand, Response>
    {
        private readonly ProductManagementDBContext _pmDBContext;
        public DeleteSubCategoryByIdHandler(ProductManagementDBContext pmDBContext)
        {
            _pmDBContext = pmDBContext;
        }
        public async Task<Response> Handle(DeleteSubCategoryByIdCommand request, CancellationToken cancellationToken)
        {
            var subCategory = await _pmDBContext.SubCategories.FindAsync(request.Id);
            if (subCategory == null)
                return new Response(false, "SubCategory not found!");
            _pmDBContext.SubCategories.Remove(subCategory);
            await _pmDBContext.SaveChangesAsync();

            return new Response(true, "Successfully Deleted!");
        }
    }
}
