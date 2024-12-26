using InnoShop.CommonLibrary.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Commands.ProductCommands
{
    public class ChangeProductStatusesOfProductsByUserIdCommand : IRequest<Response>
    {
        public Guid UserId { get; set; }
    }
}
