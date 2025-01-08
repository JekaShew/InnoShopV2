using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.Commands.RoleCommands;
using UserManagement.Application.Interfaces;

namespace UserManagement.Infrastructure.Handlers.RoleHandlers.CommandHandlers
{
    public class AddRoleHandler : IRequestHandler<AddRoleCommand, Response>
    {
        private readonly IRole _roleRepository;
        public AddRoleHandler(IRole roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<Response> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            request.RoleDTO.Id = Guid.NewGuid();

            return await _roleRepository.AddRole(request.RoleDTO);
        }
    }
}
