using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.Commands.UserCommands;
using UserManagement.Application.Interfaces;

namespace UserManagement.Infrastructure.Handlers.UserHandlers.CommandHandlers
{
    public class ChangeUserStatusOfUserHandler : IRequestHandler<ChangeUserStatusOfUserCommand, Response>
    {
        private readonly IUser _userRepository;
        private readonly IUserStatus _userStatusRepository;
        public ChangeUserStatusOfUserHandler(IUser userRepository, IUserStatus userStatusRepository)
        {
            _userRepository = userRepository;
            _userStatusRepository = userStatusRepository;
        }
        public async Task<Response> Handle(ChangeUserStatusOfUserCommand request, CancellationToken cancellationToken)
        {
            var userDTO = await _userRepository.TakeUserById(request.UserId);
            var userStatusDTO = await _userStatusRepository.TakeUserStatusById(request.UserStatusId);

            if (userDTO is null)
                return new Response(false, "Error occured while changing status! User Not Found!");

            if (userStatusDTO is null)
                return new Response(false, "Error occured while changing status! User Status Not Found!");

            userDTO.UserStatusId = request.UserStatusId;

            return await _userRepository.UpdateUser(userDTO);
        }
    }
}
