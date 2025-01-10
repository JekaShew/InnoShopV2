using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Commands.SubCategoryCommands
{
    public class AddSubCategoryCommand : IRequest<Response> 
    {
        public SubCategoryDTO SubCategoryDTO { get; set; }
    }
}
