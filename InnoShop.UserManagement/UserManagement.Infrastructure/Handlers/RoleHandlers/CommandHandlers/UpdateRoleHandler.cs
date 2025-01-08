using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.Commands.RoleCommands;
using UserManagement.Application.Interfaces;

namespace UserManagement.Infrastructure.Handlers.RoleHandlers.CommandHandlers
{
    public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, Response>
    {
        private readonly IRole _roleRepository;
        public UpdateRoleHandler(IRole roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<Response> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            return await _roleRepository.UpdateRole(request.RoleDTO);
        }
    }
}
