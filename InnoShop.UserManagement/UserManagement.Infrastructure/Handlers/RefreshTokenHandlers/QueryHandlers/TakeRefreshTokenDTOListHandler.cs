using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.DTOs;
using UserManagement.Application.Mappers;
using UserManagement.Application.Queries.RefreshQueries;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Handlers.RefreshTokenHandlers.QueryHandlers
{
    public class TakeRefreshTokenDTOListHandler : IRequestHandler<TakeRefreshTokenDTOListQuery, List<RefreshTokenDTO>>
    {
        private readonly UserManagementDBContext _umDBContext;
        public TakeRefreshTokenDTOListHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<List<RefreshTokenDTO>> Handle(TakeRefreshTokenDTOListQuery request, CancellationToken cancellationToken)
        {
            return await _umDBContext.RefreshTokens
                        .AsNoTracking()
                        .Select(rt => RefreshTokenMapper.RefreshTokenToRefreshTokenDTO(rt))
                        .ToListAsync();
        }
    }
}
