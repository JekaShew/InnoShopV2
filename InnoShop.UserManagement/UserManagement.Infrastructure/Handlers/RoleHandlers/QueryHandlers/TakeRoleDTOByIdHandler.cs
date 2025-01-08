using MediatR;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Queries.RoleQueries;

namespace UserManagement.Infrastructure.Handlers.RoleHandlers.QueryHandlers
{
    public class TakeRoleDTOByIdHandler : IRequestHandler<TakeRoleDTOByIdQuery, RoleDTO>
    {
        private readonly IRole _roleRepository;
        public TakeRoleDTOByIdHandler(IRole roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<RoleDTO> Handle(TakeRoleDTOByIdQuery request, CancellationToken cancellationToken)
        {
            return await _roleRepository.TakeRoleById(request.Id);
        }
    }
}
