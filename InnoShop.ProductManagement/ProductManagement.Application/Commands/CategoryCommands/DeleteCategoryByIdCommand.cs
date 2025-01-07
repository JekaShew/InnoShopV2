using InnoShop.CommonLibrary.Response;
using MediatR;

namespace ProductManagement.Application.Commands.CategoryCommands
{
    public class DeleteCategoryByIdCommand : IRequest<Response>
    {
        public Guid Id { get; set; }
    }
}
