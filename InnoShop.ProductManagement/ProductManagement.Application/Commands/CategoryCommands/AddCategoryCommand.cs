using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Commands.CategoryCommands
{
    public class AddCategoryCommand : IRequest<Response>
    {
        public CategoryDTO CategoryDTO { get; set; }
    }
}
