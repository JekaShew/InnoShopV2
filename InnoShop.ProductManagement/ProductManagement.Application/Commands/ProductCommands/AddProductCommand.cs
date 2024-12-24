using InnoShop.CommonLibrary.CommonDTOs;
using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Commands.ProductCommands
{
    public class AddProductCommand : IRequest<Response>
    {
        public ProductDTO ProductDTO { get; set; }
    }
}
