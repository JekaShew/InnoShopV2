using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Queries.UserQueries
{
    public class TakeAuthorizationInfoDTOByUserIdQuery : IRequest<AuthorizationInfoDTO>
    {
        public Guid UserId { get; set; }
    }
}
