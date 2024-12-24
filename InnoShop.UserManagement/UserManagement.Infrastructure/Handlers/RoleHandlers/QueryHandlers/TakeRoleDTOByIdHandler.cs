using MediatR;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.DTOs;
using UserManagement.Application.Mappers;
using UserManagement.Application.Queries.RoleQueries;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Handlers.RoleHandlers.QueryHandlers
{
    public class TakeRoleDTOByIdHandler : IRequestHandler<TakeRoleDTOByIdQuery, RoleDTO>
    {
        private readonly UserManagementDBContext _umDBContext;
        public TakeRoleDTOByIdHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<RoleDTO> Handle(TakeRoleDTOByIdQuery request, CancellationToken cancellationToken)
        {
            var roleDTO = RoleMapper.RoleToRoleDTO(await _umDBContext.Roles
                    .AsNoTracking()
                    .FirstOrDefaultAsync(r => r.Id == request.Id));

            return roleDTO;
        }
    }
}
