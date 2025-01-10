using InnoShop.CommonLibrary.Response;
using MediatR;

namespace UserManagement.Application.Commands.UserStatusCommands
{
    public class DeleteUserStatusByIdCommand : IRequest<Response>
    {
        public Guid Id { get; set; }
    }
}
