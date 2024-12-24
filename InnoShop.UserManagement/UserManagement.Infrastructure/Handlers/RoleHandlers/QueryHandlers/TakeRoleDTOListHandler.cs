using MediatR;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.DTOs;
using UserManagement.Application.Mappers;
using UserManagement.Application.Queries.RoleQueries;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Handlers.RoleHandlers.QueryHandlers
{

    public class TakeRoleDTOListHandler : IRequestHandler<TakeRoleDTOListQuery, List<RoleDTO>>
    {
        private readonly UserManagementDBContext _umDBContext;
        public TakeRoleDTOListHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<List<RoleDTO>> Handle(TakeRoleDTOListQuery request, CancellationToken cancellationToken)
        {
            var roleDTOs = await _umDBContext.Roles
                     .AsNoTracking()
                     .Select(r => RoleMapper.RoleToRoleDTO(r))
                     .ToListAsync(cancellationToken);

            return roleDTOs;
        }
    }
}
