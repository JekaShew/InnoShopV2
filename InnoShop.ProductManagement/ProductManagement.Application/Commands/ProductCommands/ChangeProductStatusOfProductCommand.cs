using InnoShop.CommonLibrary.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Commands.ProductCommands
{
    public class ChangeProductStatusOfProductCommand : IRequest<Response>
    {
        public Guid ProductId { get; set; }
        public Guid ProductStatusId { get; set; }
    }
}
