using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.DTOs;
using UserManagement.Application.Mappers;
using UserManagement.Application.Queries.UserStatusQueries;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Handlers.UserStatusHandlers.QueryHandlers
{
    public class TakeUserStatusDTOListHandler : IRequestHandler<TakeUserStatusDTOListQuery, List<UserStatusDTO>>
    {
        private readonly UserManagementDBContext _umDBContext;
        public TakeUserStatusDTOListHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<List<UserStatusDTO>> Handle(TakeUserStatusDTOListQuery request, CancellationToken cancellationToken)
        {
            var userStatusDTOs = await _umDBContext.UserStatuses
                     .AsNoTracking()
                     .Select(us => UserStatusMapper.UserStatusToUserStatusDTO(us))
                     .ToListAsync(cancellationToken);

            return userStatusDTOs;
        }
    }
}
