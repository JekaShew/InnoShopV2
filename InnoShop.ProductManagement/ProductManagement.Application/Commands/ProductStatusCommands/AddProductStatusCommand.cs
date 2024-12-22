using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Commands.ProductStatusCommands
{
    public class AddProductStatusCommand : IRequest<Response>
    {
        public ProductStatusDTO ProductStatusDTO { get; set; }
    }
}
