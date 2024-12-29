using InnoShop.CommonLibrary.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Commands.RoleCommands
{
    public class AddRoleCommand : IRequest<Response>
    {
        public RoleDTO RoleDTO { get; set; }
    }
}
