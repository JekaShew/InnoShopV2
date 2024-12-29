using InnoShop.CommonLibrary.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Commands.UserStatusCommands
{
    public class UpdateUserStatusCommand : IRequest<Response>
    {
        public UserStatusDTO UserStatusDTO { get; set; }
    }
}
