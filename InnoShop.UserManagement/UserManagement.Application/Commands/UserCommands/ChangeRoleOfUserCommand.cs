using InnoShop.CommonLibrary.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Commands.UserCommands
{
    public class ChangeRoleOfUserCommand : IRequest<Response>
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
