using InnoShop.CommonLibrary.Response;
using MediatR;

namespace ProductManagement.Application.Commands.ProductCommands
{
    public class ChangeProductStatusOfProductCommand : IRequest<Response>
    {
        public Guid ProductId { get; set; }
        public Guid ProductStatusId { get; set; }
    }
}
