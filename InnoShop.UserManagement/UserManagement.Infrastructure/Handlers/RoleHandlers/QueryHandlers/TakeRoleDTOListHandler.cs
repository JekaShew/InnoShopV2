using MediatR;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Queries.RoleQueries;

namespace UserManagement.Infrastructure.Handlers.RoleHandlers.QueryHandlers
{
    public class TakeRoleDTOListHandler : IRequestHandler<TakeRoleDTOListQuery, List<RoleDTO>>
    {
        private readonly IRole _roleRepository;
        public TakeRoleDTOListHandler(IRole roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<List<RoleDTO>> Handle(TakeRoleDTOListQuery request, CancellationToken cancellationToken)
        {
            return await _roleRepository.TakeAllRoles();
        }
    }
}
