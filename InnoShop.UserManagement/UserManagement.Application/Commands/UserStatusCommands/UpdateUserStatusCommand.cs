using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Commands.UserStatusCommands
{
    public class UpdateUserStatusCommand : IRequest<Response>
    {
        public UserStatusDTO UserStatusDTO { get; set; }
    }
}
