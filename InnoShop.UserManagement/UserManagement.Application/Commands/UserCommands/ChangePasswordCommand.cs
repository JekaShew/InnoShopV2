using InnoShop.CommonLibrary.Response;
using MediatR;

namespace UserManagement.Application.Commands.UserCommands
{
    public class ChangePasswordCommand : IRequest<Response>
    {
        public Guid UserId { get; set; }
        public string NewPasswordHash { get; set; }
    }
}
