using InnoShop.CommonLibrary.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Commands.RefreshTokenCommands;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Handlers.RefreshTokenHandlers.CommandHandlers
{
    public class RevokeRefreshTokenByRTokenIdHandler : IRequestHandler<RevokeRefreshTokenByRTokenIdCommand, Response>
    {
        private readonly UserManagementDBContext _umDBContext;
        public RevokeRefreshTokenByRTokenIdHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<Response> Handle(RevokeRefreshTokenByRTokenIdCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await _umDBContext.RefreshTokens
                                    .Where(rt => rt.Id == request.RTokenId)
                                    .FirstOrDefaultAsync();
            if (refreshToken == null)
                return new Response(false, "Refresh Token Not Found!");
            refreshToken.IsRevoked = true;

            await _umDBContext.SaveChangesAsync(cancellationToken);

            return new Response(true, "Refresh Token succesfully Revoked!");
        }
    }
}
