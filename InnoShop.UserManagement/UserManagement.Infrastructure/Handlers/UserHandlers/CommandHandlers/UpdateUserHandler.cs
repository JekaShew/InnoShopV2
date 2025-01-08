using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.Commands.UserCommands;
using UserManagement.Application.Interfaces;

namespace UserManagement.Infrastructure.Handlers.UserHandlers.CommandHandlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Response>
    {
        private readonly IUser _userRepository;
        public UpdateUserHandler(IUser userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.UpdateUser(request.UserDTO);
        }
    }
}
