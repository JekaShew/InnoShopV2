using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Commands.CategoryCommands
{
    public class AddCategoryCommand : IRequest<Response>
    {
        public CategoryDTO CategoryDTO { get; set; }
    }
}
