using InnoShop.CommonLibrary.Response;
using MediatR;

namespace ProductManagement.Application.Commands.ProductCommands
{
    public class ChangeProductStatusesOfProductsByUserIdCommand : IRequest<Response>
    {
        public Guid UserId { get; set; }
    }
}
