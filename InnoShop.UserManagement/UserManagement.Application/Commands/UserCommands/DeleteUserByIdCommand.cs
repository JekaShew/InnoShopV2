using InnoShop.CommonLibrary.Response;
using MediatR;

namespace UserManagement.Application.Commands.UserCommands
{
    public class DeleteUserByIdCommand : IRequest<Response>
    {
        public Guid Id { get; set; }
    }
}
