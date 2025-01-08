using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.Commands.UserStatusCommands;
using UserManagement.Application.Interfaces;

namespace UserManagement.Infrastructure.Handlers.UserStatusHandlers.CommandHandlers
{
    public class UpdateUserStatusHandler : IRequestHandler<UpdateUserStatusCommand, Response>
    {
        private readonly IUserStatus _userStatusRepository;
        public UpdateUserStatusHandler(IUserStatus userStatusRepository)
        {
            _userStatusRepository = userStatusRepository;
        }
        public async Task<Response> Handle(UpdateUserStatusCommand request, CancellationToken cancellationToken)
        {
            return await _userStatusRepository.UpdateUserStatus(request.UserStatusDTO);
        }
    }
}
