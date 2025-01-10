using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Commands.ProductStatusCommands
{
    public class UpdateProductStatusCommand : IRequest<Response>
    {
        public ProductStatusDTO ProductStatusDTO { get; set; }
    }
}
