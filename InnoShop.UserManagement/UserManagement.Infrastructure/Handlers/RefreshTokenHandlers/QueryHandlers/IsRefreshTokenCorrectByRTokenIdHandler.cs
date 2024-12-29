using InnoShop.CommonLibrary.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Queries.RefreshQueries;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Handlers.RefreshTokenHandlers.QueryHandlers
{
    public class IsRefreshTokenCorrectByRTokenIdHandler : IRequestHandler<IsRefreshTokenCorrectByRTokenIdQuery, Response>
    {
        private readonly UserManagementDBContext _umDBContext;
        public IsRefreshTokenCorrectByRTokenIdHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<Response> Handle(IsRefreshTokenCorrectByRTokenIdQuery request, CancellationToken cancellationToken)
        {
            var refreshToken = await _umDBContext.RefreshTokens
                                    .AsNoTracking()
                                    .Where(rt => rt.Id == request.RTokenId)
                                    .FirstOrDefaultAsync(cancellationToken);
            if (refreshToken == null)
                return new Response(false, "Refresh Token Not Found!");

            if (refreshToken.IsRevoked == true)
                return new Response(false, "Refresh Token is Revoked!");

            if (refreshToken.ExpireDate <= DateTime.UtcNow || refreshToken.ExpireDate == null)
                return new Response(false, "Refresh Token Expired!");

            return new Response(true, "Refresh Token is Correct!");
        }
    }
}
