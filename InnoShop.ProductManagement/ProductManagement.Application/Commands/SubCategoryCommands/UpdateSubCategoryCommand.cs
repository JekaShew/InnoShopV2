using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Commands.SubCategoryCommands
{
    public class UpdateSubCategoryCommand : IRequest<Response>
    {
        public SubCategoryDTO SubCategoryDTO { get; set; }
    }
}
