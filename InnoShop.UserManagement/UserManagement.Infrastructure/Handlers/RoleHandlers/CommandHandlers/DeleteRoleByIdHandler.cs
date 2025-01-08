using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.Commands.RoleCommands;
using UserManagement.Application.Interfaces;

namespace UserManagement.Infrastructure.Handlers.RoleHandlers.CommandHandlers
{
    public class DeleteRoleByIdHandler : IRequestHandler<DeleteRoleByIdCommand, Response>
    {
        private readonly IRole _roleRepository;
        public DeleteRoleByIdHandler(IRole roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<Response> Handle(DeleteRoleByIdCommand request, CancellationToken cancellationToken)
        {
            return await _roleRepository.DeleteRoleById(request.Id);
        }
    }
}
