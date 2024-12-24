using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Queries.UserStatusQueries
{
    public class TakeUserStatusDTOListQuery : IRequest<List<UserStatusDTO>>
    {
    }
}
