using InnoShop.CommonLibrary.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Commands.CategoryCommands
{
    public class DeleteCategoryByIdCommand : IRequest<Response>
    {
        public Guid Id { get; set; }
    }
}
