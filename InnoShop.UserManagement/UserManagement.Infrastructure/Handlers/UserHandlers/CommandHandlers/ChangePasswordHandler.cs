using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.Commands.UserCommands;
using UserManagement.Application.Interfaces;

namespace UserManagement.Infrastructure.Handlers.UserHandlers.CommandHandlers
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, Response>
    {
        private readonly IUser _userRepository;
        public ChangePasswordHandler(IUser userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Response> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var authorizationInfoDTO = 
                await _userRepository.TakeAuthorizationInfoDTOWithPredicate(u => u.Id == request.UserId);

            if (authorizationInfoDTO is null)
                return new Response(false, "Error occured while changing password! User not found!");

            authorizationInfoDTO.PasswordHash = request.NewPasswordHash;

            return await _userRepository.UpdateAuthorizationInfoOfUser(authorizationInfoDTO);
        }
    }
}
