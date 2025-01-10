using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Queries.RoleQueries
{
    public class TakeRoleDTOByIdQuery : IRequest<RoleDTO>
    {
        public Guid Id { get; set; }
    }
}
