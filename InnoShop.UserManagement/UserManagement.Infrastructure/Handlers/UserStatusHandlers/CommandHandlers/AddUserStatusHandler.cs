using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.Commands.UserStatusCommands;
using UserManagement.Application.Interfaces;

namespace UserManagement.Infrastructure.Handlers.UserStatusHandlers.CommandHandlers
{
    public class AddUserStatusHandler : IRequestHandler<AddUserStatusCommand, Response>
    {
        private readonly IUserStatus _userStatusRepository;
        public AddUserStatusHandler(IUserStatus userStatusRepository)
        {
            _userStatusRepository = userStatusRepository;
        }

        public async Task<Response> Handle(AddUserStatusCommand request, CancellationToken cancellationToken)
        {
            request.UserStatusDTO.Id = Guid.NewGuid();

            return await _userStatusRepository.AddUserStatus(request.UserStatusDTO);
        }
    }
}
