using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Commands.UserCommands
{
    public class AddUserCommand : IRequest<Response>
    {
        public RegistrationInfoDTO RegistrationInfoDTO { get; set; }
        public string PasswordHash { get; set; }
        public string SecretWordHash { get; set; }
        public string SecurityStamp { get; set; }

    }
}
