using InnoShop.CommonLibrary.Response;
using MediatR;

namespace ProductManagement.Application.Commands.ProductStatusCommands
{
    public class DeleteProductStatusByIdCommand : IRequest<Response>
    {
        public Guid Id { get; set; }
    }
}
