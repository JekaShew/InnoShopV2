using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Queries.UserStatusQueries
{
    public class TakeUserStatusDTOByIdQuery : IRequest<UserStatusDTO>
    {
        public Guid Id { get; set; }
    }
}
