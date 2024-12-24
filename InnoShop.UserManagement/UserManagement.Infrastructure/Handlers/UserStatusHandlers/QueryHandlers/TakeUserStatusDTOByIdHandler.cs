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
    public class TakeUserStatusDTOByIdHandler : IRequestHandler<TakeUserStatusDTOByIdQuery, UserStatusDTO>
    {
        private readonly UserManagementDBContext _umDBContext;
        public TakeUserStatusDTOByIdHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<UserStatusDTO> Handle(TakeUserStatusDTOByIdQuery request, CancellationToken cancellationToken)
        {
            var userStatusDTO = UserStatusMapper.UserStatusToUserStatusDTO(await _umDBContext.UserStatuses
                        .AsNoTracking()
                        .FirstOrDefaultAsync(us => us.Id == request.Id));

            return userStatusDTO;
        }
    }
}
