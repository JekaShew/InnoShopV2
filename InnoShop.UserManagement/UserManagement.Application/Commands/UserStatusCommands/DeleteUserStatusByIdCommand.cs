using InnoShop.CommonLibrary.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Commands.UserStatusCommands
{
    public class DeleteUserStatusByIdCommand : IRequest<Response>
    {
        public Guid Id { get; set; }
    }
}
