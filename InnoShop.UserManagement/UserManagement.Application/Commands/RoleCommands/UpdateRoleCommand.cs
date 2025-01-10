using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Commands.RoleCommands
{
    public class UpdateRoleCommand : IRequest<Response>
    {
        public RoleDTO RoleDTO { get; set; }
    }
}
