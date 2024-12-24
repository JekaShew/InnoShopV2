using InnoShop.CommonLibrary.CommonDTOs;
using InnoShop.CommonLibrary.Response;
using MediatR;

namespace ProductManagement.Application.Commands.ProductCommands
{
    public class UpdateProductCommand : IRequest<Response>
    {
        public ProductDTO ProductDTO { get; set; }
    }
}
