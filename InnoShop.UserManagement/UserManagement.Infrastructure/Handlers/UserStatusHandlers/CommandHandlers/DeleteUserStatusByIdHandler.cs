using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.Commands.UserStatusCommands;
using UserManagement.Application.Interfaces;

namespace UserManagement.Infrastructure.Handlers.UserStatusHandlers.CommandHandlers
{
    public class DeleteUserStatusByIdHandler : IRequestHandler<DeleteUserStatusByIdCommand, Response>
    {
        private readonly IUserStatus _userStatusRepository;
        public DeleteUserStatusByIdHandler(IUserStatus userStatusRepository)
        {
            _userStatusRepository = userStatusRepository;
        }
        public async Task<Response> Handle(DeleteUserStatusByIdCommand request, CancellationToken cancellationToken)
        {
            return await _userStatusRepository.DeleteUserStatusById(request.Id);
        }
    }
}
