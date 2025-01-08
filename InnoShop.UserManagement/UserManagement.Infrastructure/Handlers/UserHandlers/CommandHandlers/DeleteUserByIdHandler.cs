using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.Commands.UserCommands;
using UserManagement.Application.Interfaces;

namespace UserManagement.Infrastructure.Handlers.UserHandlers.CommandHandlers
{
    public class DeleteUserByIdHandler : IRequestHandler<DeleteUserByIdCommand, Response>
    {
        private readonly IUser _userRepository;
        public DeleteUserByIdHandler(IUser userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Response> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.DeleteUserById(request.Id);
        }
    }
}
