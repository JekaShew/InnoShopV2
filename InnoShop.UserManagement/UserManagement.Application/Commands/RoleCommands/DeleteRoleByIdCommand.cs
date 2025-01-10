using InnoShop.CommonLibrary.Response;
using MediatR;

namespace UserManagement.Application.Commands.RoleCommands
{
    public class DeleteRoleByIdCommand : IRequest<Response>
    {
        public Guid Id { get; set; }
    }
}
