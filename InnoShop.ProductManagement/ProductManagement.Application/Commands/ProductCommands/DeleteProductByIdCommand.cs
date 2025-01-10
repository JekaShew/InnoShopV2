using InnoShop.CommonLibrary.Response;
using MediatR;

namespace ProductManagement.Application.Commands.ProductCommands
{
    public class DeleteProductByIdCommand : IRequest<Response>
    {
        public Guid Id { get; set; }
    }
}
