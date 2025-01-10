using InnoShop.CommonLibrary.Response;
using MediatR;

namespace ProductManagement.Application.Commands.SubCategoryCommands
{
    public class DeleteSubCategoryByIdCommand : IRequest<Response>
    {
        public Guid Id { get; set; }
    }
}
