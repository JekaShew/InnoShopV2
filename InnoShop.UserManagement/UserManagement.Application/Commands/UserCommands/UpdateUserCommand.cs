using InnoShop.CommonLibrary.CommonDTOs;
using InnoShop.CommonLibrary.Response;
using MediatR;

namespace UserManagement.Application.Commands.UserCommands
{
    public class UpdateUserCommand : IRequest<Response>
    {
        public UserDTO UserDTO { get; set; }
    }
}
