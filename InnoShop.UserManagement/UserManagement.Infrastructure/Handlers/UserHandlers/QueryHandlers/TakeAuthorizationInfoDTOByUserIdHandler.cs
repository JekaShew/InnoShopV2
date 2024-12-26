﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.DTOs;
using UserManagement.Application.Mappers;
using UserManagement.Application.Queries.UserQueries;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Handlers.UserHandlers.QueryHandlers
{
    public class TakeAuthorizationInfoDTOByUserIdHandler : IRequestHandler<TakeAuthorizationInfoDTOByUserIdQuery, AuthorizationInfoDTO>
    {
        private readonly UserManagementDBContext _umDBContext;
        public TakeAuthorizationInfoDTOByUserIdHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<AuthorizationInfoDTO> Handle(TakeAuthorizationInfoDTOByUserIdQuery request, CancellationToken cancellationToken)
        {
            return UserMapper.UserToAuthorizationInfoDTO(await _umDBContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == request.UserId));
        }
    }
}