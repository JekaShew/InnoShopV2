using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Queries.RoleQueries
{
    public class TakeRoleDTOListQuery : IRequest<List<RoleDTO>>
    {
    }
}
