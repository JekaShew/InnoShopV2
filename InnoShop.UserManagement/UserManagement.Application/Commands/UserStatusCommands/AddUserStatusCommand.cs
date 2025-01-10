using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Commands.UserStatusCommands
{
    public class AddUserStatusCommand : IRequest<Response>
    {
        public UserStatusDTO UserStatusDTO { get; set; }
    }
}
