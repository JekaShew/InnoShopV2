using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.Commands.UserCommands;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Mappers;

namespace UserManagement.Infrastructure.Handlers.UserHandlers.CommandHandlers
{
    public class AddUserHandler : IRequestHandler<AddUserCommand, Response>
    {
        private readonly IUser _userRepository;
        public AddUserHandler(IUser userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Response> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var userDetailedDTO = UserMapper.RegistrationInfoDTOToUserDetailedDTO(request.RegistrationInfoDTO);

            userDetailedDTO.Id = Guid.NewGuid();
            userDetailedDTO.SecurityStamp = request.SecurityStamp;
            userDetailedDTO.PasswordHash = request.PasswordHash;
            userDetailedDTO.SecretWordHash = request.SecretWordHash;

            return await _userRepository.AddUser(userDetailedDTO);           
        }
    }
}
