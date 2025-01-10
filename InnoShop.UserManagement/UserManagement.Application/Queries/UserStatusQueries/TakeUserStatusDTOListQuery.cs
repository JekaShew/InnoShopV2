using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Queries.UserStatusQueries
{
    public class TakeUserStatusDTOListQuery : IRequest<List<UserStatusDTO>>
    {
    }
}
