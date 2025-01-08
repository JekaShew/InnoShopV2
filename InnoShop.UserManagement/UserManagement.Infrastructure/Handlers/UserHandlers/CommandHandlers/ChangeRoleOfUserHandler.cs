using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.Commands.UserCommands;
using UserManagement.Application.Interfaces;

namespace UserManagement.Infrastructure.Handlers.UserHandlers.CommandHandlers
{
    public class ChangeRoleOfUserHandler : IRequestHandler<ChangeRoleOfUserCommand, Response>
    {
        private readonly IUser _userRepository;
        private readonly IRole _roleRepository;
        public ChangeRoleOfUserHandler(IUser userRepository, IRole roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        public async Task<Response> Handle(ChangeRoleOfUserCommand request, CancellationToken cancellationToken)
        {
            var userDTO = await _userRepository.TakeUserById(request.UserId);
            var roleDTO = await _roleRepository.TakeRoleById(request.RoleId);

            if (userDTO is null)
                return new Response(false, "Error occured while changing role! User Not Found!");
            if (roleDTO is null)
                return new Response(false, "Error occured while changing role! Role Not Found!");

            userDTO.RoleId = request.RoleId;

            return await _userRepository.UpdateUser(userDTO);
        }
    }
}
