using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Queries.UserQueries
{
    public class TakeAuthorizationInfoDTOByLoginQuery : IRequest<AuthorizationInfoDTO>
    {
        public string EnteredLogin { get; set; }
    }
}
